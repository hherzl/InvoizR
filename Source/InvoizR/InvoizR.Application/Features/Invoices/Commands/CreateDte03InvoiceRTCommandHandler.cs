using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeCcf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices.Commands;

public class CreateDte03InvoiceRTCommandHandler : IRequestHandler<CreateDte03InvoiceRTCommand, CreatedResponse<long?>>
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly IInvoizRDbContext _dbContext;
    private readonly Dte14ProcessingService _dteProcessingService;
    private readonly ISeguridadClient _seguridadClient;
    private readonly DteHandler _dteHandler;
    private readonly IEnumerable<IInvoiceExportStrategy> _invoiceExportStrategies;

    public CreateDte03InvoiceRTCommandHandler
    (
        ILogger<CreateDte01InvoiceRTCommandHandler> logger,
        IConfiguration configuration,
        IInvoizRDbContext dbContext,
        Dte14ProcessingService dteProcessingService,
        ISeguridadClient seguridadClient,
        DteHandler dteHandler,
        IEnumerable<IInvoiceExportStrategy> invoiceExportStrategies
    )
    {
        _logger = logger;
        _configuration = configuration;
        _dbContext = dbContext;
        _dteProcessingService = dteProcessingService;
        _seguridadClient = seguridadClient;
        _dteHandler = dteHandler;
        _invoiceExportStrategies = invoiceExportStrategies;
    }

    public async Task<CreatedResponse<long?>> Handle(CreateDte03InvoiceRTCommand request, CancellationToken cancellationToken)
    {
        _ = await _dbContext.GetCurrentInvoiceTypeAsync(FeCcfv3.TypeId, ct: cancellationToken) ?? throw new InvalidCurrentInvoiceTypeException();

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
                InvoiceTypeId = FeCcfv3.TypeId,
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

            var mhSettings = new MhSettings();
            _configuration.Bind("Clients:Mh", mhSettings);

            var processingSettings = new ProcessingSettings();
            _configuration.Bind("ProcessingSettings", processingSettings);

            var authRequest = new AuthRequest();
            _configuration.Bind("Clients:Mh", authRequest);

            var authResponse = await _seguridadClient.AuthAsync(authRequest);

            var createDteRequest = CreateDte03Request.Create(mhSettings, processingSettings, authResponse.Body.Token, invoice.Id, invoice.Payload);
            var flag = await _dteHandler.HandleAsync(createDteRequest, _dbContext, cancellationToken);
            if (!flag)
                return new(invoice.Id);

            foreach (var item in _invoiceExportStrategies)
            {
                _logger.LogInformation($"Exporting '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}' invoice as '{item.FileExtension}'...");
                var bytes = await item.ExportAsync(invoice, processingSettings.GetDtePath(invoice.ControlNumber, item.FileExtension), cancellationToken);

                _logger.LogInformation($" Adding '{item.FileExtension}' as bytes...");
                _dbContext.InvoiceFile.Add(InvoiceFileHelper.Create(invoice, bytes, item.ContentType, item.FileExtension));
            }

            var invoiceType = await _dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: cancellationToken);
            var notificationTemplate = new DteNotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
            var notificationPath = processingSettings.GetDteNotificationPath(invoice.ControlNumber);

            _logger.LogInformation($"Creating notification file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{notificationPath}'...");

            await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), cancellationToken);

            if (string.IsNullOrEmpty(invoice.CustomerEmail))
                invoice.CustomerEmail = "sinfactura@capsule-corp.com";

            _dbContext.InvoiceNotification.Add(new(invoice.Id, invoice.CustomerEmail, false, 2, true));

            var notifications = await _dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, invoice.InvoiceTypeId).ToListAsync(cancellationToken);
            foreach (var notification in notifications)
            {
                if (notification.Bcc == true)
                    notificationTemplate.Model.Bcc.Add(notification.Email);
                else
                    notificationTemplate.Model.Copies.Add(notification.Email);

                _dbContext.InvoiceNotification.Add(new(invoice.Id, notification.Email, notification.Bcc, 2, true));
            }

            _logger.LogInformation($"Sending notification for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}'; customer '{invoice.CustomerName}', email: '{invoice.CustomerEmail}'...");

            //smtpClient.Send(notificationTemplate.ToMailMessage());

            // TODO: emit notification for webhook

            invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Notified;

            _dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new(invoice.Id);
        }
        catch (Exception ex)
        {
            await txn.RollbackAsync(cancellationToken);

            _logger.LogCritical(ex, "There was an error on Create DTE-03 Invoice in RT processing");

            return new();
        }
    }
}
