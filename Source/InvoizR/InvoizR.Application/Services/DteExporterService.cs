using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Features.Invoices.Notifications;
using InvoizR.Application.Helpers;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Clients.Contracts;
using InvoizR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public class DteExporterService
{
    private readonly ILogger _logger;
    private readonly IEnumerable<IInvoiceExportStrategy> _invoiceExportStrategies;
    private readonly IConfiguration _configuration;
    private readonly IInvoizRDbContext _dbContext;
    private readonly ISmtpClient _smtpClient;

    public DteExporterService
    (
        ILogger<ExportInvoiceNotificationHandler> logger,
        IInvoizRDbContext dbContext,
        IConfiguration configuration,
        IEnumerable<IInvoiceExportStrategy> invoiceExportStrategies,
        ISmtpClient smtpClient
    )
    {
        _logger = logger;
        _dbContext = dbContext;
        _configuration = configuration;
        _invoiceExportStrategies = invoiceExportStrategies;
        _smtpClient = smtpClient;
    }

    public async Task ExportAsync(long? invoiceId, CancellationToken cancellationToken = default)
    {
        var invoice = await _dbContext.GetInvoiceAsync(invoiceId, true, true, cancellationToken);
        var invoiceType = await _dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: cancellationToken);

        var processingSettings = new ProcessingSettings();
        _configuration.Bind("ProcessingSettings", processingSettings);

        foreach (var item in _invoiceExportStrategies)
        {
            _logger.LogInformation($"Exporting '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}' invoice as '{item.FileExtension}'...");
            var bytes = await item.ExportAsync(invoice, processingSettings.GetDtePath(invoice.ControlNumber, item.FileExtension), cancellationToken);

            _logger.LogInformation($" Adding '{item.FileExtension}' as bytes...");

            _dbContext.InvoiceFile.Add(InvoiceFileHelper.Create(invoice, bytes, item.ContentType, item.FileExtension));
        }

        var notificationTemplate = new DteNotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
        var notificationPath = processingSettings.GetDteNotificationPath(invoice.ControlNumber);

        _logger.LogInformation($"Creating notification file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{notificationPath}'...");

        await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), cancellationToken);

        if (string.IsNullOrEmpty(invoice.CustomerEmail))
            invoice.CustomerEmail = "sinfactura@capsule-corp.com";

        _dbContext.InvoiceNotification.Add(new(invoice.Id, invoice.CustomerEmail, false, (short)_invoiceExportStrategies.Count(), true));

        var branchNotifications = await _dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, invoice.InvoiceTypeId).ToListAsync(cancellationToken);
        foreach (var branchNotification in branchNotifications)
        {
            if (branchNotification.Bcc == true)
                notificationTemplate.Model.Bcc.Add(branchNotification.Email);
            else
                notificationTemplate.Model.Copies.Add(branchNotification.Email);

            _dbContext.InvoiceNotification.Add(new(invoice.Id, branchNotification.Email, branchNotification.Bcc, (short)_invoiceExportStrategies.Count(), true));
        }

        _logger.LogInformation($"Sending notification for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}'; customer '{invoice.CustomerName}', email: '{invoice.CustomerEmail}'...");

        _smtpClient.Send(notificationTemplate.ToMailMessage());

        invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Notified;

        _dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
