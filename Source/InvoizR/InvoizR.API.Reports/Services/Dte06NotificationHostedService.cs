using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh.FeNd;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Reports.Services;

public sealed class Dte06NotificationHostedService(ILogger<Dte06NotificationHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var invoiceExporter = scope.ServiceProvider.GetRequiredService<InvoiceExporter>();

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var filters = new Filters(FeNdv3.TypeId)
                    .Set(InvoiceProcessingType.OneWay)
                    .Add(InvoiceProcessingStatus.Processed)
                    ;

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingTypeId, [.. filters.ProcessingStatuses]).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    logger.LogInformation($"There are no '{FeNdv3.SchemaType}' invoices to process...");
                    continue;
                }

                logger.LogInformation($"Exporting '{invoices.Count}' invoices ...");

                foreach (var item in invoices)
                {
                    await invoiceExporter.ExportAsync(item.Id, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"There was on error exporting DTE-06 invoices");
            }
        }
    }
}
