using InvoizR.Application.Common;
using InvoizR.Application.Services;
using InvoizR.Domain.Enums;
using InvoizR.Infrastructure.Persistence;
using InvoizR.SharedKernel.Mh.FeFc;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Reports.Services;

public sealed class Dte01NotificationHostedService(ILogger<Dte01NotificationHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken st)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<InvoizRDbContext>();

        var invoiceExporter = serviceScope.ServiceProvider.GetRequiredService<InvoiceExporter>();

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(st))
        {
            try
            {
                var filters = new Filters(FeFcv1.TypeId)
                    .Set(InvoiceProcessingType.OneWay)
                    .Add(InvoiceProcessingStatus.Processed)
                    ;

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingTypeId, [.. filters.ProcessingStatuses]).ToListAsync(st);
                if (invoices.Count == 0)
                {
                    logger.LogInformation($"There are no '{FeFcv1.SchemaType}' invoices to process...");
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
                logger.LogCritical(ex, $"There was on error exporting DTE-01 invoices");
            }
        }
    }
}
