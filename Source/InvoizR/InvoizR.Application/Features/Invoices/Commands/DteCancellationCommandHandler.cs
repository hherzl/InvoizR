using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts.Cancellation;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class DteCancellationCommandHandler : IRequestHandler<DteCancellationCommand, Response>
{
    private readonly ILogger _logger;
    private readonly IInvoizRDbContext _dbContext;
    private readonly ISeguridadClient _seguridadClient;
    private readonly DteCancellationHandler _dteCancellationHandler;

    public DteCancellationCommandHandler
    (
        ILogger<DteCancellationCommandHandler> logger,
        IInvoizRDbContext dbContext,
        ISeguridadClient seguridadClient,
        DteCancellationHandler dteCancellationHandler
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _seguridadClient = seguridadClient;
        _dteCancellationHandler = dteCancellationHandler;
    }

    public async Task<Response> Handle(DteCancellationCommand request, CancellationToken st)
    {
        var invoice = await _dbContext.GetInvoiceAsync(request.InvoiceId, includes: true, ct: st);
        if (invoice == null)
            return null;

        var processingStatuses = new short?[] { (short)InvoiceProcessingStatus.Processed, (short)InvoiceProcessingStatus.Notified };
        if (!processingStatuses.Contains(invoice.ProcessingStatusId))
            throw new InvalidInvoiceCancellationException();

        var invoiceType = await _dbContext.GetCurrentInvoiceTypeAsync(invoice.InvoiceTypeId, ct: st);
        if (DateTime.Now.Subtract(invoice.EmitDateTime.Value).TotalDays > invoiceType.CancellationPeriodInDays)
            throw new InvalidInvoiceCancellationException();

        var responsible = await _dbContext.GetResponsibleByCompanyIdAsync(invoice.Pos.Branch.Company.Id, ct: st);
        if (responsible == null || responsible.AuthorizeCancellation == false)
            throw new NoResponsibleForInvoiceCancellationException();

        var thirdPartyServices = await _dbContext.ThirdPartyServices(invoice.Pos.Branch.Company.Environment, includes: true).ToListAsync(st);
        if (thirdPartyServices.Count == 0)
            throw new NoThirdPartyServicesException(invoice.Pos.Branch.Company.Name, invoice.Pos.Branch.Company.Environment);

        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
        _seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(_seguridadClient.ServiceName).ToSeguridadClientSettings();
        var authResponse = await _seguridadClient.AuthAsync();
        thirdPartyServicesParameters.AddJwt(invoice.Pos.Branch.Company.Environment, authResponse.Body.Token);

        if (await _dteCancellationHandler.HandleAsync(new CancelDteRequest(thirdPartyServicesParameters, invoice.Id, request.Anulacion, responsible), _dbContext, st: st))
            await _dbContext.DispatchNotificationsAsync(st);

        return new();
    }
}
