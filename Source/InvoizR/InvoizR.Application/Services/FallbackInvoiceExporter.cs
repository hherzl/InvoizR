using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Clients.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class FallbackInvoiceExporter(ILogger<FallbackInvoiceExporter> logger, IServiceProvider serviceProvider, IConfiguration configuration)
{
    public async Task ExportAsync(long? invoiceId, CancellationToken ct = default)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var invoiceExportStrategies = serviceScope.ServiceProvider.GetServices<IInvoiceExportStrategy>();
        var smtpClient = serviceScope.ServiceProvider.GetRequiredService<ISmtpClient>();

        var invoice = await dbContext.GetInvoiceAsync(invoiceId, true, true, ct);
        var invoiceType = await dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: ct);

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        foreach (var item in invoiceExportStrategies)
        {
            logger.LogInformation($"Exporting '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}' invoice as '{item.FileExtension}'...");

            var bytes = await item.ExportAsync(invoice, processingSettings.GetInvoicePath(invoice.AuditNumber, item.FileExtension), ct);

            logger.LogInformation($" Adding '{item.FileExtension}' as bytes...");

            dbContext.InvoiceFiles.Add(new(invoice, bytes, item.ContentType, item.FileExtension));
        }

        var notificationTemplate = new DteNotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
        var notificationPath = processingSettings.GetInvoiceNotificationPath(invoice.AuditNumber);

        logger.LogInformation($"Creating notification file for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}', path: '{notificationPath}'...");

        await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), ct);

        if (!invoice.HasCustomerEmail)
            invoice.CustomerEmail = invoice.Pos.Branch.NonCustomerEmail;

        dbContext.InvoiceNotifications.Add(new(invoice.Id, invoice.CustomerEmail, false, (short)invoiceExportStrategies.Count(), true));

        var branchNotifications = await dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, invoice.InvoiceTypeId).ToListAsync(ct);
        foreach (var branchNotification in branchNotifications)
        {
            if (branchNotification.Bcc == true)
                notificationTemplate.Model.Bcc.Add(branchNotification.Email);
            else
                notificationTemplate.Model.Copies.Add(branchNotification.Email);

            dbContext.InvoiceNotifications.Add(new(invoice.Id, branchNotification.Email, branchNotification.Bcc, (short)invoiceExportStrategies.Count(), true));
        }

        logger.LogInformation($"Sending notification for invoice '{invoice.InvoiceTypeId}-{invoice.InvoiceNumber}'; customer '{invoice.CustomerName}', email: '{invoice.CustomerEmail}'...");

        smtpClient.Send(notificationTemplate.ToMailMessage());

        await dbContext.SaveChangesAsync(ct);
    }
}
