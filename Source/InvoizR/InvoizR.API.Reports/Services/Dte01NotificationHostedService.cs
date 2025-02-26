using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.API.Reports.Helpers;
using InvoizR.API.Reports.Templates.Pdf;
using InvoizR.API.Reports.Templates.Smtp;
using InvoizR.Application.Common;
using InvoizR.Application.Helpers;
using InvoizR.Clients.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Reports.Services;

public class Dte01NotificationHostedService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public Dte01NotificationHostedService(ILogger<Dte01NotificationHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            using var scope = _serviceProvider.CreateScope();

            try
            {
                using var dbContext = scope.ServiceProvider.GetRequiredService<InvoizRDbContext>();

                var filters = new
                {
                    InvoiceTypeId = (short)1,
                    ProcessingStatuses = new short?[]
                    {
                        (short)InvoiceProcessingStatus.Processed
                    }
                };

                var invoices = await dbContext.GetInvoicesBy(filters.InvoiceTypeId, filters.ProcessingStatuses).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    _logger.LogInformation($"There are no invoices to process...");
                    continue;
                }

                var processingSettings = new ProcessingSettings();
                _configuration.Bind("ProcessingSettings", processingSettings);

                var converter = scope.ServiceProvider.GetRequiredService<IConverter>();

                var smtpClient = scope.ServiceProvider.GetRequiredService<ISmtpClient>();

                foreach (var invoice in invoices)
                {
                    var model = Dte01TemplateFactory.Create(invoice);

                    var jsonPath = processingSettings.GetDteJsonPath(invoice.ControlNumber);
                    await File.WriteAllTextAsync(jsonPath, invoice.Serialization, stoppingToken);

                    var pdfPath = processingSettings.GetDtePdfPath(invoice.ControlNumber);
                    var pdf = new HtmlToPdfDocument
                    {
                        GlobalSettings = DinkToPdfHelper.CreateDteGlobalSettings(pdfPath),
                        Objects =
                        {
                            DinkToPdfHelper.CreateDteObjSettings(new DteTemplatev1(model).ToString())
                        }
                    };

                    converter.Convert(pdf);

                    var invoiceFiles = await dbContext.GetInvoiceFiles(invoice.Id).ToListAsync(stoppingToken);
                    if (invoiceFiles.Count == 0)
                    {
                        dbContext.InvoiceFile.Add(await InvoiceFileHelper.CreateJsonAsync(invoice, jsonPath, stoppingToken));
                        dbContext.InvoiceFile.Add(await InvoiceFileHelper.CreatePdfAsync(invoice, pdfPath, stoppingToken));
                    }

                    await dbContext.SaveChangesAsync(stoppingToken);

                    var invoiceType = await dbContext.GetInvoiceTypeAsync(invoice.InvoiceTypeId, ct: stoppingToken);
                    var notificationTemplate = new Dte01NotificationTemplatev1(new(invoice.Pos.Branch, invoiceType, invoice));
                    var notificationPath = processingSettings.GetDteNotificationPath(invoice.ControlNumber);
                    await File.WriteAllTextAsync(notificationPath, notificationTemplate.ToString(), stoppingToken);

                    dbContext.InvoiceNotification.Add(new(invoice.Id, invoice.CustomerEmail, false, 2, true));

                    var notifications = await dbContext.GetBranchNotificationsBy(invoice.Pos.BranchId, invoice.InvoiceTypeId).ToListAsync(stoppingToken);
                    foreach (var notification in notifications)
                    {
                        if (notification.Bcc == true)
                            notificationTemplate.Model.Bcc.Add(notification.Email);
                        else
                            notificationTemplate.Model.Copies.Add(notification.Email);

                        dbContext.InvoiceNotification.Add(new(invoice.Id, notification.Email, notification.Bcc, 2, true));
                    }

                    smtpClient.Send(notificationTemplate.ToMailMessage());

                    // TODO: emit notification for webhook

                    invoice.ProcessingStatusId = (short)InvoiceProcessingStatus.Notified;

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
