using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel.Mh.FeFc;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public sealed class CreateDte01InvoiceRTCommandHandler : IRequestHandler<CreateDte01InvoiceRTCommand, CreatedResponse<long?>>
{
    private readonly ILogger _logger;
    private readonly IInvoizRDbContext _dbContext;
    private readonly Dte01ProcessingService _dteProcessingService;
    private readonly IConfiguration _configuration;
    private readonly ISeguridadClient _seguridadClient;
    private readonly DteHandler _dteHandler;

    public CreateDte01InvoiceRTCommandHandler
    (
        ILogger<CreateDte01InvoiceRTCommandHandler> logger,
        IInvoizRDbContext dbContext,
        Dte01ProcessingService dteProcessingService,
        IConfiguration configuration,
        ISeguridadClient seguridadClient,
        DteHandler dteHandler
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _dteProcessingService = dteProcessingService;
        _configuration = configuration;
        _seguridadClient = seguridadClient;
        _dteHandler = dteHandler;
    }

    public async Task<CreatedResponse<long?>> Handle(CreateDte01InvoiceRTCommand request, CancellationToken cancellationToken)
    {
        _ = await _dbContext.GetCurrentInvoiceTypeAsync(FeFcv1.TypeId, ct: cancellationToken) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var pos = await _dbContext.GetPosAsync(request.PosId, ct: cancellationToken);

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

            var mhSettings = new MhSettings();
            _configuration.Bind("Clients:Mh", mhSettings);

            var processingSettings = new ProcessingSettings();
            _configuration.Bind("ProcessingSettings", processingSettings);

            var authRequest = new AuthRequest();
            _configuration.Bind("Clients:Mh", authRequest);

            var authResponse = await _seguridadClient.AuthAsync(authRequest);

            var createDteRequest = CreateDte01Request.Create(mhSettings, processingSettings, authResponse.Body.Token, invoice.Id, invoice.Payload);
            if (await _dteHandler.HandleAsync(createDteRequest, _dbContext, cancellationToken))
            {
                invoice.AddNotification(new ExportInvoiceNotification(invoice));

                await _dbContext.DispatchNotificationsAsync(cancellationToken);
            }

            return new(invoice.Id);
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(cancellationToken);
            _logger.LogCritical(ex, "There was an error on Create DTE-01 Invoice in RT processing");
            return new();
        }
    }
}
