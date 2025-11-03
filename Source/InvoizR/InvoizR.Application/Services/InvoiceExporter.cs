using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Helpers;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Clients.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class InvoiceExporter(ILogger<InvoiceExporter> logger, IEnumerable<IInvoiceExportStrategy> exportStrategies, IConfiguration configuration, IInvoizRDbContext dbContext, ISmtpClient smtpClient)
{
    public async Task ExportAsync(long? invoiceId, CancellationToken cancellationToken = default)
    {
        var invoice = await dbContext.GetInvoiceAsync(invoiceId, true, true, cancellationToken);
        var invoiceType = await dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: cancellationToken);
        var pos = await dbContext.GetPosAsync(invoice.PosId, includes: true, ct: cancellationToken);

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        foreach (var exportStrategy in exportStrategies)
        {
            logger.LogInformation($"Exporting '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}' invoice as '{exportStrategy.FileExtension}'...");

            var bytes = await exportStrategy.ExportAsync(invoice, processingSettings.GetDtePath(invoice.AuditNumber, exportStrategy.FileExtension), cancellationToken);

            logger.LogInformation($" Adding '{exportStrategy.FileExtension}' as bytes...");

            var onTheFlyFile = new InvoiceFile(invoice, bytes, exportStrategy.ContentType, exportStrategy.FileExtension);
            var existingFile = await dbContext.InvoiceFile.FirstOrDefaultAsync(item => item.InvoiceId == invoice.Id && item.FileName == onTheFlyFile.FileName, cancellationToken);
            if (existingFile == null)
                dbContext.InvoiceFile.Add(onTheFlyFile);
            else
                existingFile.File = onTheFlyFile.File;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var notificationTemplate = new DteNotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
        var notificationPath = processingSettings.GetDteNotificationPath(invoice.AuditNumber);

        logger.LogInformation($"Creating notification file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{notificationPath}'...");

        await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), cancellationToken);

        if (!invoice.HasCustomerEmail)
            invoice.CustomerEmail = pos.Branch.NonCustomerEmail;

        dbContext.InvoiceNotification.Add(new(invoice.Id, invoice.CustomerEmail, false, (short)exportStrategies.Count(), true));

        var branchNotifications = await dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, invoice.InvoiceTypeId).ToListAsync(cancellationToken);
        foreach (var branchNotification in branchNotifications)
        {
            if (branchNotification.Bcc == true)
                notificationTemplate.Model.Bcc.Add(branchNotification.Email);
            else
                notificationTemplate.Model.Copies.Add(branchNotification.Email);

            dbContext.InvoiceNotification.Add(new(invoice.Id, branchNotification.Email, branchNotification.Bcc, (short)exportStrategies.Count(), true));
        }

        logger.LogInformation($"Sending notification for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}'; customer '{invoice.CustomerName}', email: '{invoice.CustomerEmail}'...");

        smtpClient.Send(notificationTemplate.ToMailMessage());

        invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Notified;

        dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
