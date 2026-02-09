using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNc;
using InvoizR.SharedKernel.Mh.FeNd;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.API.Billing.Services;

public sealed class InvoizRInitializer(IInvoizRDbContext dbContext)
{
    public async Task SeedAsync()
    {
        if (!dbContext.ThirdPartyServices.Any())
        {
            dbContext.ThirdPartyServices.Add(new("LC", "Seguridad", "Seguridad")
            {
                ThirdPartyServiceParameters =
                [
                    new(ThirdPatyClientParameterTypes.Url, "Endpoint", "https://localhost:7200"),
                    new(ThirdPatyClientParameterTypes.Header, "User-Agent", "InvoizR Seguridad client"),
                    new(ThirdPatyClientParameterTypes.Body, "User", "06140101211234"),
                    new(ThirdPatyClientParameterTypes.Body, "Pwd", "P@$$w0rd3")
                ]
            });

            await dbContext.SaveChangesAsync();

            dbContext.ThirdPartyServices.Add(new("LC", "Firmador", "Firmador")
            {
                ThirdPartyServiceParameters =
                [
                    new(ThirdPatyClientParameterTypes.Url, "Endpoint", "https://localhost:7210"),
                    new(ThirdPatyClientParameterTypes.Header, "User-Agent", "InvoizR Firmador client"),
                    new(ThirdPatyClientParameterTypes.Body, "PrivateKey", "CapsuleCorpPr1v4t3K3y")
                ]
            });

            await dbContext.SaveChangesAsync();

            dbContext.ThirdPartyServices.Add(new("LC", "FE SV", "FE SV")
            {
                ThirdPartyServiceParameters =
                [
                    new(ThirdPatyClientParameterTypes.Url, "Endpoint", "https://localhost:7220"),
                    new(ThirdPatyClientParameterTypes.Header, "User-Agent", "InvoizR FE SV client"),
                    new(ThirdPatyClientParameterTypes.Url, "PublicQuery", "https://admin.factura.gob.sv/consultaPublica?ambiente=env&codGen=guid&fechaEmi=emitDate")
                ]
            });

            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.InvoiceTypes.Any())
        {
            dbContext.InvoiceTypes.Add(new(FeFcv1.TypeId, "Consumidor Final", FeFcv1.SchemaType, FeFcv1.Version, true, 90));
            dbContext.InvoiceTypes.Add(new(FeCcfv3.TypeId, "Comprobante de Crédito Fiscal", FeCcfv3.SchemaType, FeCcfv3.Version, true, 1));
            dbContext.InvoiceTypes.Add(new(FeNrv3.TypeId, "Nota de Remisión", FeNrv3.SchemaType, FeNrv3.Version, true, 90));
            dbContext.InvoiceTypes.Add(new(FeNcv3.TypeId, "Nota de Crédito", FeNcv3.SchemaType, FeNcv3.Version, true, 90));
            dbContext.InvoiceTypes.Add(new(FeNdv3.TypeId, "Nota de Débito", FeNdv3.SchemaType, FeNdv3.Version, true, 90));
            dbContext.InvoiceTypes.Add(new(FeFsev1.TypeId, "Factura Sujeto Excluido", FeFsev1.SchemaType, FeFsev1.Version, true, 90));

            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.EnumDescriptions.Any())
        {
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Created, "Creada", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.InvalidSchema, "Esquema inválido", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.InvalidData, "Datos inválidos", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Initialized, "Inicializada", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Fallback, "Contingencia", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Requested, "Solicitada", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Declined, "Rechazada", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Processed, "Procesada", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Cancelled, "Inválida", typeof(SyncStatus).FullName));
            dbContext.EnumDescriptions.Add(new((short)SyncStatus.Notified, "Notificada", typeof(SyncStatus).FullName));

            dbContext.EnumDescriptions.Add(new((short)InvoiceProcessingType.RoundTrip, "RT", typeof(InvoiceProcessingType).FullName));
            dbContext.EnumDescriptions.Add(new((short)InvoiceProcessingType.OneWay, "OW", typeof(InvoiceProcessingType).FullName));

            await dbContext.SaveChangesAsync();
        }
    }
}
