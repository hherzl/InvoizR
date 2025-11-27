using InvoizR.Application;
using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeCcf;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Billing.Services;

public sealed class Dte03HostedService(ILogger<Dte03HostedService> logger, IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken st)
    {
        using var serviceScope = serviceProvider.CreateScope();

        using var dbContext = serviceScope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var seguridadClient = serviceScope.ServiceProvider.GetRequiredService<ISeguridadClient>();
        var hubContext = serviceScope.ServiceProvider.GetRequiredService<IHubContext<BillingHub>>();

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(st))
        {
            try
            {
                var filters = new Filters(FeCcfv3.TypeId)
                    .Set(InvoiceProcessingType.OneWay)
                    .Add(SyncStatus.Created, SyncStatus.Initialized, SyncStatus.Requested)
                    ;

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingTypeId, [.. filters.SyncStatuses]).ToListAsync(st);
                if (invoices.Count == 0)
                {
                    logger.LogInformation($"There are no invoices (DTE-03 OW) to sync...");
                    continue;
                }

                var dteSyncStatusChanger = serviceScope.ServiceProvider.GetRequiredService<Dte03SyncStatusChanger>();
                var invoiceSyncHandler = serviceScope.ServiceProvider.GetRequiredService<InvoiceSyncHandler>();

                foreach (var invoice in invoices)
                {
                    if (invoice.SyncStatusId == (short)SyncStatus.Created)
                    {
                        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Created}'...");

                        await dteSyncStatusChanger.SetInvoiceAsInitializedAsync(invoice.Id, dbContext, st);
                    }
                    else if (invoice.SyncStatusId == (short)SyncStatus.Initialized)
                    {
                        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Initialized}'...");

                        await dteSyncStatusChanger.SetInvoiceAsRequestedAsync(invoice.Id, dbContext, st);
                    }
                    else if (invoice.SyncStatusId == (short)SyncStatus.Requested)
                    {
                        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Requested}'...");

                        var thirdPartyServices = await dbContext.GetThirdPartyServices(invoice.Environment, includes: true).ToListAsync(st);
                        if (thirdPartyServices.Count == 0)
                            throw new NoThirdPartyServicesException(invoice.Company, invoice.Environment);

                        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
                        seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(seguridadClient.ServiceName).ToSeguridadClientSettings();
                        var authResponse = await seguridadClient.AuthAsync();
                        thirdPartyServicesParameters.AddJwt(invoice.Environment, authResponse.Body.Token);

                        if (await invoiceSyncHandler.HandleAsync(CreateDte03Request.Create(thirdPartyServicesParameters, invoice.Id, invoice.Payload), dbContext, st))
                        {
                            logger.LogInformation($"Broadcasting '{invoice.InvoiceNumber}' invoice...");
                            await hubContext.Clients.All.SendAsync(HubMethods.SendInvoice, invoice.InvoiceTypeId, invoice.InvoiceNumber, invoice.InvoiceTotal, st);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"There was on error processing DTE-03 in OW");
            }
        }
    }
}
