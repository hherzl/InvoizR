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

namespace InvoizR.Application.Features.Invoices;

public sealed class DteCancellationCommandHandler(IInvoizRDbContext dbContext, ISeguridadClient seguridadClient, InvoiceCancellationHandler dteCancellationHandler)
    : IRequestHandler<DteCancellationCommand, Response>
{
    public async Task<Response> Handle(DteCancellationCommand request, CancellationToken ct)
    {
        var invoice = await dbContext.GetInvoiceAsync(request.InvoiceId, includes: true, ct: ct);
        if (invoice == null)
            return null;

        var syncStatuses = new short?[] { (short)SyncStatus.Processed, (short)SyncStatus.Notified };
        if (!syncStatuses.Contains(invoice.SyncStatusId))
            throw new InvalidInvoiceCancellationException();

        var invoiceType = await dbContext.GetCurrentInvoiceTypeAsync(invoice.InvoiceTypeId, ct: ct);
        if (DateTime.Now.Subtract(invoice.EmitDateTime.Value).TotalDays > invoiceType.CancellationPeriodInDays)
            throw new InvalidInvoiceCancellationException();

        var responsible = await dbContext.GetResponsibleByCompanyIdAsync(invoice.Pos.Branch.Company.Id, ct: ct);
        if (responsible == null || responsible.AuthorizeCancellation == false)
            throw new NoResponsibleForInvoiceCancellationException();

        var thirdPartyServices = await dbContext.GetThirdPartyServices(invoice.Pos.Branch.Company.Environment, includes: true).ToListAsync(ct);
        if (thirdPartyServices.Count == 0)
            throw new NoThirdPartyServicesException(invoice.Pos.Branch.Company.Name, invoice.Pos.Branch.Company.Environment);

        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
        seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(seguridadClient.ServiceName).ToSeguridadClientSettings();
        var authResponse = await seguridadClient.AuthAsync();
        thirdPartyServicesParameters.AddJwt(invoice.Pos.Branch.Company.Environment, authResponse.Body.Token);

        if (await dteCancellationHandler.HandleAsync(new CancelDteRequest(thirdPartyServicesParameters, invoice.Id, request.Anulacion, responsible), dbContext, ct: ct))
            await dbContext.DispatchNotificationsAsync(ct);

        return new();
    }
}
