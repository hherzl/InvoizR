using InvoizR.Application.Common;
using InvoizR.Application.Services;
using InvoizR.Domain.Enums;
using InvoizR.Infrastructure.Persistence;
using InvoizR.SharedKernel.Mh.FeFse;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Reports.Services;

public class Dte14NotificationHostedService(ILogger<Dte14NotificationHostedService> logger, IServiceProvider serviceProvider, IConfiguration configuration) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<InvoizRDbContext>();

        var dteExporter = scope.ServiceProvider.GetRequiredService<DteExporterService>();

        var processingSettings = new ProcessingSettings();
        configuration.Bind("ProcessingSettings", processingSettings);

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var filters = new
                {
                    FeFsev1.TypeId,
                    ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                    ProcessingStatuses = new short?[]
                    {
                        (short)InvoiceProcessingStatus.Processed
                    }
                };

                var invoices = await dbContext.GetInvoicesForProcessing(filters.TypeId, filters.ProcessingTypeId, filters.ProcessingStatuses).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    logger.LogInformation($"There are no '{FeFsev1.SchemaType}' invoices to process...");
                    continue;
                }

                logger.LogInformation($"Exporting '{invoices.Count}' invoices ...");

                foreach (var item in invoices)
                {
                    await dteExporter.ExportAsync(item.Id, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"There was on error exporting DTE-14 invoices");
            }
        }
    }
}
