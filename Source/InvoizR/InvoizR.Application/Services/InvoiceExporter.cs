using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Clients.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class InvoiceExporter(ILogger<InvoiceExporter> logger, IServiceProvider serviceProvider, IConfiguration configuration)
{
    public async Task ExportAsync(long? invoiceId, CancellationToken ct = default)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var exportStrategies = serviceScope.ServiceProvider.GetServices<IInvoiceExportStrategy>();
        var smtpClient = serviceScope.ServiceProvider.GetRequiredService<ISmtpClient>();

        var invoice = await dbContext.GetInvoiceAsync(invoiceId, true, true, ct);
        var invoiceType = await dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: ct);

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        foreach (var exportStrategy in exportStrategies)
        {
            logger.LogInformation($"Exporting '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}' invoice as '{exportStrategy.FileExtension}'...");

            var bytes = await exportStrategy.ExportAsync(invoice, processingSettings.GetInvoicePath(invoice.AuditNumber, exportStrategy.FileExtension), ct);

            logger.LogInformation($" Adding '{exportStrategy.FileExtension}' as bytes...");

            var onTheFlyFile = new InvoiceFile(invoice, bytes, exportStrategy.ContentType, exportStrategy.FileExtension);
            var existingFile = await dbContext.GetInvoiceFileByAsync(invoice.Id, onTheFlyFile.FileName, ct: ct);
            if (existingFile == null)
                dbContext.InvoiceFiles.Add(onTheFlyFile);
            else
                existingFile.File = onTheFlyFile.File;
        }

        await dbContext.SaveChangesAsync(ct);

        var notificationTemplate = new DteNotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
        var notificationPath = processingSettings.GetInvoiceNotificationPath(invoice.AuditNumber);

        logger.LogInformation($"Creating notification file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{notificationPath}'...");

        await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), ct);

        if (!invoice.HasCustomerEmail)
            invoice.CustomerEmail = invoice.Pos.Branch.NonCustomerEmail;

        dbContext.InvoiceNotifications.Add(new(invoice.Id, invoice.CustomerEmail, false, (short)exportStrategies.Count(), true));

        var branchNotifications = await dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, invoice.InvoiceTypeId).ToListAsync(ct);
        foreach (var branchNotification in branchNotifications)
        {
            if (branchNotification.Bcc == true)
                notificationTemplate.Model.Bcc.Add(branchNotification.Email);
            else
                notificationTemplate.Model.Copies.Add(branchNotification.Email);

            dbContext.InvoiceNotifications.Add(new(invoice.Id, branchNotification.Email, branchNotification.Bcc, (short)exportStrategies.Count(), true));
        }

        logger.LogInformation($"Sending notification for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}'; customer '{invoice.CustomerName}', email: '{invoice.CustomerEmail}'...");

        smtpClient.Send(notificationTemplate.ToMailMessage());

        invoice.SyncStatusId = (short)SyncStatus.Notified;

        dbContext.InvoiceSyncStatusLogs.Add(new(invoice.Id, invoice.SyncStatusId));

        await dbContext.SaveChangesAsync(ct);
    }
}
