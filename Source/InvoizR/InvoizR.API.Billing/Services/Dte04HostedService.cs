using InvoizR.Application;
using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh.FeNr;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.API.Billing.Services;

public sealed class Dte04HostedService(ILogger<Dte04HostedService> logger, IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken st)
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var seguridadClient = scope.ServiceProvider.GetRequiredService<ISeguridadClient>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<BillingHub>>();

        var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while (await timer.WaitForNextTickAsync(st))
        {
            try
            {
                var filters = new Filters(FeNrv3.TypeId)
                    .Set(InvoiceProcessingType.OneWay)
                    .Add(InvoiceProcessingStatus.Created, InvoiceProcessingStatus.Initialized, InvoiceProcessingStatus.Requested)
                    ;

                var invoices = await dbContext.GetInvoicesForProcessing(filters.InvoiceTypeId, filters.ProcessingTypeId, [.. filters.ProcessingStatuses]).ToListAsync(st);
                if (invoices.Count == 0)
                {
                    logger.LogInformation($"There are no invoices (DTE-04 OW) to sync...");
                    continue;
                }

                var dteProcessingStatusChanger = scope.ServiceProvider.GetRequiredService<Dte04SyncStatusChanger>();
                var dteSyncHandler = scope.ServiceProvider.GetRequiredService<DteSyncHandler>();

                foreach (var invoice in invoices)
                {
                    if (invoice.ProcessingStatusId == (short)InvoiceProcessingStatus.Created)
                    {
                        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Created}'...");

                        await dteProcessingStatusChanger.SetInvoiceAsInitializedAsync(invoice.Id, dbContext, st);
                    }
                    else if (invoice.ProcessingStatusId == (short)InvoiceProcessingStatus.Initialized)
                    {
                        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Initialized}'...");

                        await dteProcessingStatusChanger.SetInvoiceAsRequestedAsync(invoice.Id, dbContext, st);
                    }
                    else if (invoice.ProcessingStatusId == (short)InvoiceProcessingStatus.Requested)
                    {
                        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{InvoiceProcessingStatus.Requested}'...");

                        var thirdPartyServices = await dbContext.ThirdPartyServices(invoice.Environment, includes: true).ToListAsync(st);
                        if (thirdPartyServices.Count == 0)
                            throw new NoThirdPartyServicesException(invoice.Company, invoice.Environment);

                        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
                        seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(seguridadClient.ServiceName).ToSeguridadClientSettings();
                        var authResponse = await seguridadClient.AuthAsync();
                        thirdPartyServicesParameters.AddJwt(invoice.Environment, authResponse.Body.Token);

                        if (await dteSyncHandler.HandleAsync(CreateDte04Request.Create(thirdPartyServicesParameters, invoice.Id, invoice.Payload), dbContext, st))
                        {
                            logger.LogInformation($"Broadcasting '{invoice.InvoiceNumber}' invoice...");
                            await hubContext.Clients.All.SendAsync(HubMethods.SendInvoice, invoice.InvoiceTypeId, invoice.InvoiceNumber, invoice.InvoiceTotal, st);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"There was on error processing DTE-04 in OW");
            }
        }
    }
}
