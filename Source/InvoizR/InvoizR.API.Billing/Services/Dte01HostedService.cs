using InvoizR.Application.Common;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Enums;
using InvoizR.Infrastructure.Persistence;
using InvoizR.SharedKernel.Mh.FeFc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Billing.Services;

public class Dte01HostedService : BackgroundService
{
    private readonly ILogger _logger;
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
        using var scope = _serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<InvoizRDbContext>();

        var mhSettings = new MhSettings();
        _configuration.Bind("Clients:Mh", mhSettings);

        var processingSettings = new ProcessingSettings();
        _configuration.Bind("ProcessingSettings", processingSettings);

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var filters = new
                {
                    FeFcv1.TypeId,
                    ProcessingTypeId = (short)InvoiceProcessingType.OneWay,
                    ProcessingStatuses = new short?[]
                    {
                        (short)InvoiceProcessingStatus.Created,
                        (short)InvoiceProcessingStatus.Initialized,
                        (short)InvoiceProcessingStatus.Requested
                    }
                };

                var invoices = await dbContext.GetInvoicesForProcessing(filters.TypeId, filters.ProcessingTypeId, filters.ProcessingStatuses).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    _logger.LogInformation($"There are no invoices (OW) to sync...");
                    continue;
                }

                var authRequest = new AuthRequest();
                _configuration.Bind("Clients:Mh", authRequest);

                var authResponse = await _seguridadClient.AuthAsync(authRequest);

                var dteProcessingStatusChanger = scope.ServiceProvider.GetRequiredService<Dte01ProcessingStatusChanger>();
                var dteSyncHandler = scope.ServiceProvider.GetRequiredService<DteSyncHandler>();

                foreach (var invoice in invoices)
                {
                    if (invoice.ProcessingStatusId == (short)InvoiceProcessingStatus.Created)
                    {
                        _logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Created}'...");

                        await dteProcessingStatusChanger.SetInvoiceAsInitializedAsync(invoice.Id, dbContext, stoppingToken);
                    }
                    else if (invoice.ProcessingStatusId == (short)InvoiceProcessingStatus.Initialized)
                    {
                        _logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Initialized}'...");

                        await dteProcessingStatusChanger.SetInvoiceAsRequestedAsync(invoice.Id, dbContext, stoppingToken);
                    }
                    else if (invoice.ProcessingStatusId == (short)InvoiceProcessingStatus.Requested)
                    {
                        _logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Requested}'...");

                        var request = CreateDte01Request.Create(mhSettings, processingSettings, authResponse.Body.Token, invoice.Id, invoice.Payload);
                        var result = await dteSyncHandler.HandleAsync(request, dbContext, stoppingToken);
                        if (result)
                        {
                            _logger.LogInformation($"Broadcasting '{invoice.InvoiceNumber}' invoice...");
                            await _hubContext.Clients.All.SendAsync(HubMethods.SendInvoice, invoice.InvoiceTypeId, invoice.InvoiceNumber, invoice.InvoiceTotal, stoppingToken);
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
