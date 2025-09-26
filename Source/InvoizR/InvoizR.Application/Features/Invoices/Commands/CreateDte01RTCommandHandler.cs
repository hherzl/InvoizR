using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Dte01;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel.Mh.FeFc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class CreateDte01RTCommandHandler : IRequestHandler<CreateDte01RTCommand, CreatedResponse<long?>>
{
    private readonly ILogger _logger;
    private readonly IInvoizRDbContext _dbContext;
    private readonly Dte01ProcessingStatusChanger _dteProcessingService;
    private readonly ISeguridadClient _seguridadClient;
    private readonly DteSyncHandler _dteHandler;

    public CreateDte01RTCommandHandler
    (
        ILogger<CreateDte01RTCommandHandler> logger,
        IInvoizRDbContext dbContext,
        Dte01ProcessingStatusChanger dteProcessingService,
        ISeguridadClient seguridadClient,
        DteSyncHandler dteHandler
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _dteProcessingService = dteProcessingService;
        _seguridadClient = seguridadClient;
        _dteHandler = dteHandler;
    }

    public async Task<CreatedResponse<long?>> Handle(CreateDte01RTCommand request, CancellationToken cancellationToken)
    {
        _ = await _dbContext.GetCurrentInvoiceTypeAsync(FeFcv1.TypeId, ct: cancellationToken) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var pos = await _dbContext.GetPosAsync(request.PosId, includes: true, ct: cancellationToken);

            var invoice = new Invoice
            {
                PosId = pos.Id,
                CustomerId = request.Customer.Id,
                CustomerDocumentTypeId = request.Customer.DocumentTypeId,
                CustomerDocumentNumber = request.Customer.DocumentNumber,
                CustomerWtId = request.Customer.WtId,
                CustomerName = request.Customer.Name,
                CustomerCountryId = request.Customer.CountryId,
                CustomerCountryLevelId = request.Customer.CountryLevelId,
                CustomerAddress = request.Customer.Address,
                CustomerPhone = request.Customer.Phone,
                CustomerEmail = request.Customer.Email,
                CustomerLastUpdated = DateTime.Now,
                InvoiceTypeId = FeFcv1.TypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Payload = request.Dte.ToJson(),
                ProcessingTypeId = (short)InvoiceProcessingType.RoundTrip,
                ProcessingStatusId = (short)InvoiceProcessingStatus.Created
            };

            _dbContext.Invoice.Add(invoice);

            await _dbContext.SaveChangesAsync(cancellationToken);

            _dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            await txn.CommitAsync(cancellationToken);

            _logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Created}'...");

            await _dteProcessingService.SetInvoiceAsInitializedAsync(invoice.Id, _dbContext, cancellationToken);

            _logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Initialized}'...");

            await _dteProcessingService.SetInvoiceAsRequestedAsync(invoice.Id, _dbContext, cancellationToken);

            _logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Requested}'...");

            var thirdPartyServices = await _dbContext.ThirdPartyServices(pos.Branch.Company.Environment, includes: true).ToListAsync(cancellationToken);
            if (thirdPartyServices.Count == 0)
                throw new NoThirdPartyServicesException(pos.Branch.Company.Name, pos.Branch.Company.Environment);

            var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
            _seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(_seguridadClient.ServiceName).ToSeguridadClientSettings();
            var authResponse = await _seguridadClient.AuthAsync();
            thirdPartyServicesParameters.AddJwt(pos.Branch.Company.Environment, authResponse.Body.Token);

            if (await _dteHandler.HandleAsync(CreateDte01Request.Create(thirdPartyServicesParameters, invoice.Id, invoice.Payload), _dbContext, cancellationToken))
            {
                invoice.AddNotification(new ExportInvoiceNotification(invoice));
                await _dbContext.DispatchNotificationsAsync(cancellationToken);
            }

            return new(invoice.Id);
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(cancellationToken);
            _logger.LogCritical(ex, "There was an error on Create DTE-01 in RT processing");
            return new();
        }
    }
}
