using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts.Dte14;
using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.Domain.Notifications;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.FeFse;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Features.Invoices;

public sealed class CreateDte14RTCommandHandler(ILogger<CreateDte14RTCommandHandler> logger, IServiceProvider serviceProvider)
    : IRequestHandler<CreateDte14RTCommand, CreatedInvoiceResponse>
{
    public async Task<CreatedInvoiceResponse> Handle(CreateDte14RTCommand request, CancellationToken ct)
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<IInvoizRDbContext>();
        var dteSyncStatusChanger = scope.ServiceProvider.GetRequiredService<Dte14SyncStatusChanger>();
        var seguridadClient = scope.ServiceProvider.GetRequiredService<ISeguridadClient>();
        var dteSyncHandler = scope.ServiceProvider.GetRequiredService<InvoiceSyncHandler>();

        _ = await dbContext.GetCurrentInvoiceTypeAsync(FeFsev1.TypeId, ct: ct) ?? throw new InvalidCurrentInvoiceTypeException();

        var txn = await dbContext.Database.BeginTransactionAsync(ct);

        Domain.Entities.Pos pos;
        Invoice invoice;

        try
        {
            pos = await dbContext.GetPosAsync(request.PosId, includes: true, ct: ct);

            invoice = new Invoice
            {
                PosId = pos.Id,
                CustomerId = request.Customer.Id,
                CustomerDocumentTypeId = request.Customer.DocumentTypeId,
                CustomerDocumentNumber = request.Customer.DocumentNumber,
                CustomerWtId = request.Customer.WtId,
                CustomerName = request.Customer.Name,
                CustomerCountryId = request.Customer.CountryId,
                CustomerCountryLevelId = request.Customer.CountryLevelId,
                CustomerAddress = request.Customer.Address,
                CustomerPhone = request.Customer.Phone,
                CustomerEmail = request.Customer.Email,
                CustomerLastUpdated = DateTime.Now,
                InvoiceTypeId = FeFsev1.TypeId,
                InvoiceNumber = request.InvoiceNumber,
                InvoiceDate = request.InvoiceDate,
                InvoiceTotal = request.InvoiceTotal,
                Lines = request.Lines,
                Payload = request.Dte.ToJson(),
                ProcessingTypeId = (short)InvoiceProcessingType.RoundTrip,
                SyncStatusId = (short)SyncStatus.Created
            };

            dbContext.Invoice.Add(invoice);

            await dbContext.SaveChangesAsync(ct);

            dbContext.InvoiceSyncStatusLog.Add(new(invoice.Id, invoice.SyncStatusId));

            await dbContext.SaveChangesAsync(ct);

            await txn.CommitAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "There was an error on Create DTE-14 in RT processing");
            await txn.RollbackAsync(ct);
            return new();
        }

        logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Created}'...");

        await dteSyncStatusChanger.SetInvoiceAsInitializedAsync(invoice.Id, dbContext, ct);

        var fallback = await dbContext.GetCurrentFallbackAsync(pos.Branch.Company.Id, ct: ct);
        if (fallback?.Enable == true)
        {
            var dte = FeFsev1.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Contingencia;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Contingencia;

            invoice.Payload = dte.ToJson();

            logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Initialized}'...");

            invoice.FallbackId = fallback.Id;
            await dteSyncStatusChanger.SetInvoiceAsFallbackAsync(invoice.Id, dbContext, ct);

            invoice.AddNotification(new ExportFallbackInvoiceNotification(invoice));
            await dbContext.DispatchNotificationsAsync(ct);
        }
        else
        {
            logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Initialized}'...");

            await dteSyncStatusChanger.SetInvoiceAsRequestedAsync(invoice.Id, dbContext, ct);

            logger.LogInformation($"Processing '{invoice.InvoiceNumber}' invoice, changing status from '{SyncStatus.Requested}'...");

            var thirdPartyServices = await dbContext.GetThirdPartyServices(pos.Branch.Company.Environment, includes: true).ToListAsync(ct);
            if (thirdPartyServices.Count == 0)
                throw new NoThirdPartyServicesException(pos.Branch.Company.Name, pos.Branch.Company.Environment);

            var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
            seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(seguridadClient.ServiceName).ToSeguridadClientSettings();
            var authResponse = await seguridadClient.AuthAsync();
            thirdPartyServicesParameters.AddJwt(pos.Branch.Company.Environment, authResponse.Body.Token);

            if (await dteSyncHandler.HandleAsync(CreateDte14Request.Create(thirdPartyServicesParameters, invoice.Id, invoice.Payload), dbContext, ct))
            {
                invoice.AddNotification(new ExportInvoiceNotification(invoice));
                await dbContext.DispatchNotificationsAsync(ct);
            }
        }

        return invoice.ToCreateResponse();
    }
}
