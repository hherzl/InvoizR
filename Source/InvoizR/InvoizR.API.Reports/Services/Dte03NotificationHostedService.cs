using InvoizR.Application.Common;
using InvoizR.Application.Services;
using InvoizR.Domain.Enums;
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

        var dteExporter = scope.ServiceProvider.GetRequiredService<DteExporter>();

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

                _logger.LogInformation($"Exporting '{invoices.Count}' invoices ...");

                foreach (var item in invoices)
                {
                    await dteExporter.ExportAsync(item.Id, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"There was on error exporting DTE-03 invoices");
            }
        }
    }
}
