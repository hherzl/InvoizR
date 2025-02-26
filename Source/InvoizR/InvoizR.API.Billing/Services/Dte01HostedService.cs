using InvoizR.Application.Common;
using InvoizR.Application.Services;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Enums;
using InvoizR.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Billing.Services;

public class Dte01HostedService : BackgroundService
{
    private readonly ILogger<Dte01HostedService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private readonly ISeguridadClient _seguridadClient;
    private readonly IHubContext<BillingHub> _hubContext;

    public Dte01HostedService
    (
        ILogger<Dte01HostedService> logger,
        IConfiguration configuration,
        IServiceProvider serviceProvider,
        ISeguridadClient seguridadClient,
        IHubContext<BillingHub> hubContext
    )
    {
        _logger = logger;
        _configuration = configuration;
        _serviceProvider = serviceProvider;
        _seguridadClient = seguridadClient;
        _hubContext = hubContext;
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
                        (short)InvoiceProcessingStatus.Created,
                        (short)InvoiceProcessingStatus.Initialized,
                        (short)InvoiceProcessingStatus.Requested
                    }
                };

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingStatuses).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    _logger.LogInformation($"There are no invoices to sync...");
                    continue;
                }

                var authRequest = new AuthRequest();
                _configuration.Bind("Clients:Mh", authRequest);

                var authResponse = await _seguridadClient.AuthAsync(authRequest);

                var mhSettings = new MhSettings();
                _configuration.Bind("Clients:Mh", mhSettings);

                var processingSettings = new ProcessingSettings();
                _configuration.Bind("ProcessingSettings", processingSettings);

                var invProcessingService = scope.ServiceProvider.GetRequiredService<InvoiceProcessingService>();

                var dteHandler = scope.ServiceProvider.GetRequiredService<DteHandler>();

                foreach (var inv in invoices)
                {
                    if (inv.ProcessingStatusId == (short)InvoiceProcessingStatus.Created)
                    {
                        _logger.LogInformation($"Processing '{inv.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Created}'...");

                        await invProcessingService.SetInvoiceAsInitializedAsync(inv.Id, dbContext, stoppingToken);
                    }
                    else if (inv.ProcessingStatusId == (short)InvoiceProcessingStatus.Initialized)
                    {
                        _logger.LogInformation($"Processing '{inv.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Initialized}'...");

                        await invProcessingService.SetInvoiceAsRequestedAsync(inv.Id, dbContext, stoppingToken);
                    }
                    else if (inv.ProcessingStatusId == (short)InvoiceProcessingStatus.Requested)
                    {
                        _logger.LogInformation($"Processing '{inv.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Requested}'...");

                        var result = await dteHandler.HandleAsync(new(mhSettings, processingSettings, authResponse.Body.Token, inv.Id), dbContext, stoppingToken);
                        if (result)
                        {
                            _logger.LogInformation($"Broadcasting '{inv.InvoiceNumber}' invoice...");
                            await _hubContext.Clients.All.SendAsync(HubMethods.SendInvoice, inv.InvoiceTypeId, inv.InvoiceNumber, inv.InvoiceTotal, stoppingToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"There was on error processing DTE-01");
            }
        }
    }
}
