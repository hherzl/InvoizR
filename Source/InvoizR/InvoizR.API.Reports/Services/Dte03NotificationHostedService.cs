using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel.Mh.FeCcf;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Reports.Services;

public sealed class Dte03NotificationHostedService(ILogger<Dte03NotificationHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken st)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var invoiceExporter = serviceScope.ServiceProvider.GetRequiredService<InvoiceExporter>();

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(st))
        {
            try
            {
                var filters = new Filters(FeCcfv3.TypeId)
                    .Set(InvoiceProcessingType.OneWay)
                    .Add(InvoiceProcessingStatus.Processed)
                    ;

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingTypeId, [.. filters.ProcessingStatuses]).ToListAsync(st);
                if (invoices.Count == 0)
                {
                    logger.LogInformation($"There are no '{FeCcfv3.SchemaType}' invoices to process...");
                    continue;
                }

                logger.LogInformation($"Exporting '{invoices.Count}' invoices ...");

                foreach (var invoice in invoices)
                {
                    await invoiceExporter.ExportAsync(invoice.Id, st);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"There was on error exporting DTE-03 invoices");
            }
        }
    }
}
