using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Clients.DataContracts.Fallback;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Entities;
using InvoizR.Domain.Enums;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel.Mh;
using InvoizR.SharedKernel.Mh.Contingencia;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNc;
using InvoizR.SharedKernel.Mh.FeNd;
using InvoizR.SharedKernel.Mh.FeNr;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Fallbacks;

public sealed class ProcessFallbackCommandHandler(IInvoizRDbContext dbContext, ISeguridadClient seguridadClient, FallbackSyncHandler fallbackSyncHandler)
    : IRequestHandler<ProcessFallbackCommand, Response>
{
    public async Task<Response> Handle(ProcessFallbackCommand request, CancellationToken ct)
    {
        var fallback = await dbContext.GetFallbackAsync(request.Id, tracking: true, includes: true, ct: ct);

        var dte = Contingenciav3.Deserialize(fallback.Payload);

        dte.Motivo.FFin = new DateTimeOffset(DateTime.Now);
        dte.Motivo.HFin = DateTime.Now.ToShortTimeString();

        dte.DetalleDTE.Clear();

        var invoices = await dbContext.GetInvoicesBy(request.Id).ToListAsync(ct);
        var details = invoices.OrderBy(item => item.InvoiceDate).Select(item => new { item.InvoiceGuid, item.SchemaType }).ToList();
        for (var i = 0; i < details.Count; i++)
        {
            var detail = details[i];
            dte.DetalleDTE.Add(new() { NoItem = i + 1, CodigoGeneracion = detail.InvoiceGuid, TipoDoc = detail.SchemaType });
        }

        fallback.Payload = dte.ToJson();

        await dbContext.SaveChangesAsync(ct);

        foreach (var invoice in invoices)
        {
            invoice.SyncStatusId = (short)SyncStatus.Requested;
            invoice.ProcessingTypeId = (short)InvoiceProcessingType.OneWay;

            UpdatePayload(invoice);

            await dbContext.SaveChangesAsync(ct);
        }

        var thirdPartyServices = await dbContext.GetThirdPartyServices(fallback.Company.Environment, includes: true).ToListAsync(ct);
        if (thirdPartyServices.Count == 0)
            throw new NoThirdPartyServicesException(fallback.Company.Name, fallback.Company.Environment);

        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
        seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(seguridadClient.ServiceName).ToSeguridadClientSettings();
        var authResponse = await seguridadClient.AuthAsync();
        thirdPartyServicesParameters.AddJwt(fallback.Company.Environment, authResponse.Body.Token);

        if (await fallbackSyncHandler.HandleAsync(new(thirdPartyServicesParameters, fallback.Id, fallback.Payload), dbContext, ct))
            await dbContext.DispatchNotificationsAsync(ct);

        return new();
    }

    private static void UpdatePayload(Invoice invoice)
    {
        if (invoice.InvoiceTypeId == FeFcv1.TypeId)
        {
            var dte = FeFcv1.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

            invoice.Payload = dte.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeCcfv3.TypeId)
        {
            var dte = FeCcfv3.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

            invoice.Payload = dte.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeNrv3.TypeId)
        {
            var dte = FeNrv3.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

            invoice.Payload = dte.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeNcv3.TypeId)
        {
            var dte = FeNcv3.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

            invoice.Payload = dte.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeNdv3.TypeId)
        {
            var dte = FeNdv3.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

            invoice.Payload = dte.ToJson();
        }
        else if (invoice.InvoiceTypeId == FeFsev1.TypeId)
        {
            var dte = FeFsev1.Deserialize(invoice.Payload);
            dte.Identificacion.TipoModelo = MhCatalog.Cat003.Previo;
            dte.Identificacion.TipoOperacion = MhCatalog.Cat004.Normal;

            invoice.Payload = dte.ToJson();
        }
        else
            throw new NotImplementedException($"There is no implementation for {invoice.InvoiceTypeId} invoice type");
    }
}
