using InvoizR.Application;
using InvoizR.Application.Common;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.Infrastructure.Persistence;
using InvoizR.SharedKernel.Mh.FeFse;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Billing.Services;

public class Dte14HostedService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ISeguridadClient _seguridadClient;
    private readonly IHubContext<BillingHub> _hubContext;

    public Dte14HostedService(ILogger<Dte14HostedService> logger, IServiceProvider serviceProvider, ISeguridadClient seguridadClient, IHubContext<BillingHub> hubContext)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _seguridadClient = seguridadClient;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<InvoizRDbContext>();

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                var filters = new Filters(FeFsev1.TypeId)
                    .Set(InvoiceProcessingType.OneWay)
                    .Add(InvoiceProcessingStatus.Created, InvoiceProcessingStatus.Initialized, InvoiceProcessingStatus.Requested)
                    ;

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingTypeId, [.. filters.ProcessingStatuses]).ToListAsync(stoppingToken);
                if (invoices.Count == 0)
                {
                    _logger.LogInformation($"There are no invoices (OW) to sync...");
                    continue;
                }

                var dteProcessingStatusChanger = scope.ServiceProvider.GetRequiredService<Dte14ProcessingStatusChanger>();
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

                        var thirdPartyServices = await dbContext.ThirdPartyServices(invoice.Environment, includes: true).ToListAsync(stoppingToken);
                        if (thirdPartyServices.Count == 0)
                            throw new NoThirdPartyServicesException(invoice.Company, invoice.Environment);

                        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
                        _seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(_seguridadClient.ServiceName).ToSeguridadClientSettings();
                        var authResponse = await _seguridadClient.AuthAsync();
                        thirdPartyServicesParameters.AddJwt(invoice.Environment, authResponse.Body.Token);

                        if (await dteSyncHandler.HandleAsync(CreateDte14Request.Create(thirdPartyServicesParameters, invoice.Id, invoice.Payload), dbContext, stoppingToken))
                        {
                            _logger.LogInformation($"Broadcasting '{invoice.InvoiceNumber}' invoice...");
                            await _hubContext.Clients.All.SendAsync(HubMethods.SendInvoice, invoice.InvoiceTypeId, invoice.InvoiceNumber, invoice.InvoiceTotal, stoppingToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"There was on error processing DTE-14 in OW");
            }
        }
    }
}
