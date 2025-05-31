using InvoizR.Application.Common;
using InvoizR.Application.Helpers;
using InvoizR.Application.Reports.Templates;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Clients.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Infrastructure.FileExport;
using InvoizR.Infrastructure.Persistence;
using InvoizR.SharedKernel.Mh.FeCcf;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Reports.Services;

public class Dte03NotificationHostedService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public Dte03NotificationHostedService(ILogger<Dte03NotificationHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<InvoizRDbContext>();

        var processingSettings = new ProcessingSettings();
        _configuration.Bind("ProcessingSettings", processingSettings);

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var filters = new
                {
                    FeCcfv3.TypeId,
                    ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                    ProcessingStatuses = new short?[]
                    {
                        (short)InvoiceProcessingStatus.Processed
                    }
                };

                var invoices = await dbContext.GetInvoicesForProcessing(filters.TypeId, filters.ProcessingTypeId, filters.ProcessingStatuses).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    _logger.LogInformation($"There are no '{FeCcfv3.SchemaType}' invoices to process...");
                    continue;
                }

                _logger.LogInformation($"Processing '{invoices.Count}' invoices ...");

                var smtpClient = scope.ServiceProvider.GetRequiredService<ISmtpClient>();

                var dte01TemplateFactory = scope.ServiceProvider.GetRequiredService<Dte01TemplateFactory>();

                var jsonInvoiceExportStrategy = scope.ServiceProvider.GetRequiredService<JsonInvoiceExportStrategy>();
                var pdfInvoiceExportStrategy = scope.ServiceProvider.GetRequiredService<PdfInvoiceExportStrategy>();

                foreach (var item in invoices)
                {
                    var invoice = await dbContext.GetInvoiceAsync(item.Id, true, true, stoppingToken);

                    var jsonBytes = await jsonInvoiceExportStrategy.ExportAsync(invoice, processingSettings.GetDteJsonPath(item.ControlNumber), stoppingToken);
                    var pdfBytes = await pdfInvoiceExportStrategy.ExportAsync(invoice, processingSettings.GetDtePdfPath(item.ControlNumber), stoppingToken);

                    var invoiceFiles = await dbContext.GetInvoiceFiles(item.Id).ToListAsync(stoppingToken);
                    if (invoiceFiles.Count == 0)
                    {
                        dbContext.InvoiceFile.Add(InvoiceFileHelper.CreateJson(invoice, jsonBytes));
                        dbContext.InvoiceFile.Add(InvoiceFileHelper.CreatePdf(invoice, pdfBytes));
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);

                    var invoiceType = await dbContext.GetInvoiceTypeAsync(item.InvoiceTypeId, ct: stoppingToken);
                    var notificationTemplate = new DteNotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
                    var notificationPath = processingSettings.GetDteNotificationPath(item.ControlNumber);

                    _logger.LogInformation($"Creating notification file for invoice '{item.InvoiceTypeId}-{item.InvoiceNumber}', path: '{notificationPath}'...");

                    await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), stoppingToken);

                    if (string.IsNullOrEmpty(item.CustomerEmail))
                        item.CustomerEmail = "sinfactura@capsule-corp.com";

                    dbContext.InvoiceNotification.Add(new(item.Id, item.CustomerEmail, false, 2, true));

                    var notifications = await dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, item.InvoiceTypeId).ToListAsync(stoppingToken);
                    foreach (var notification in notifications)
                    {
                        if (notification.Bcc == true)
                            notificationTemplate.Model.Bcc.Add(notification.Email);
                        else
                            notificationTemplate.Model.Copies.Add(notification.Email);

                        dbContext.InvoiceNotification.Add(new(item.Id, notification.Email, notification.Bcc, 2, true));
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);

                    _logger.LogInformation($"Sending notification for invoice '{item.InvoiceTypeId}-{item.InvoiceNumber}'; customer '{item.CustomerName}', email: '{item.CustomerEmail}'...");

                    smtpClient.Send(notificationTemplate.ToMailMessage());

                    // TODO: emit notification for webhook

                    invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Notified;

                    dbContext.InvoiceProcessingStatusLog.Add(new(invoice.Id, invoice.ProcessingStatusId));

                    await dbContext.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"There was on error processing notifications for DTE-01");
            }
        }
    }
}
