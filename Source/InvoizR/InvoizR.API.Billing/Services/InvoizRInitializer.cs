using InvoizR.Application.Common.Contracts;
using InvoizR.Domain.Enums;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeCcf;
using InvoizR.SharedKernel.Mh.FeFc;
using InvoizR.SharedKernel.Mh.FeFse;
using InvoizR.SharedKernel.Mh.FeNc;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.API.Billing.Services;

public class InvoizRInitializer
{
    private readonly IInvoizRDbContext _dbContext;

    public InvoizRInitializer(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if (!_dbContext.ThirdPartyService.Any())
        {
            _dbContext.ThirdPartyService.Add(new("LC", "Seguridad", "Seguridad")
            {
                ThirdPartyServiceParameters =
                [
                    new(ThirdPatyClientParameterTypes.Url, "Endpoint", "https://localhost:7200"),
                    new(ThirdPatyClientParameterTypes.Header, "User-Agent", "InvoizR Seguridad client"),
                    new(ThirdPatyClientParameterTypes.Body, "User", "06140101211234"),
                    new(ThirdPatyClientParameterTypes.Body, "Pwd", "P@$$w0rd3")
                ]
            });

            await _dbContext.SaveChangesAsync();

            _dbContext.ThirdPartyService.Add(new("LC", "Firmador", "Firmador")
            {
                ThirdPartyServiceParameters =
                [
                    new(ThirdPatyClientParameterTypes.Url, "Endpoint", "https://localhost:7210"),
                    new(ThirdPatyClientParameterTypes.Header, "User-Agent", "InvoizR Firmador client"),
                    new(ThirdPatyClientParameterTypes.Body, "PrivateKey", "CapsuleCorpPr1v4t3K3y")
                ]
            });

            await _dbContext.SaveChangesAsync();

            _dbContext.ThirdPartyService.Add(new("LC", "FE SV", "FE SV")
            {
                ThirdPartyServiceParameters =
                [
                    new(ThirdPatyClientParameterTypes.Url, "Endpoint", "https://localhost:7220"),
                    new(ThirdPatyClientParameterTypes.Header, "User-Agent", "InvoizR FE SV client"),
                    new(ThirdPatyClientParameterTypes.Url, "PublicQuery", "https://admin.factura.gob.sv/consultaPublica?ambiente=env&codGen=genCode&fechaEmi=emitDate")
                ]
            });

            await _dbContext.SaveChangesAsync();
        }

        if (!_dbContext.InvoiceType.Any())
        {
            _dbContext.InvoiceType.Add(new(FeFcv1.TypeId, "Consumidor Final", FeFcv1.SchemaType, FeFcv1.Version, true));
            _dbContext.InvoiceType.Add(new(FeCcfv3.TypeId, "Comprobante de Crédito Fiscal", FeCcfv3.SchemaType, FeCcfv3.Version, true));
            _dbContext.InvoiceType.Add(new(FeNrv3.TypeId, "Nota de Remisión", FeNrv3.SchemaType, FeNrv3.Version, true));
            _dbContext.InvoiceType.Add(new(FeFsev1.TypeId, "Factura Sujeto Excluido", FeFsev1.SchemaType, FeFsev1.Version, true));
            _dbContext.InvoiceType.Add(new(FeNcv3.TypeId, "Nota de Crédito", FeNcv3.SchemaType, FeNcv3.Version, true));

            await _dbContext.SaveChangesAsync();
        }

        if (!_dbContext.EnumDescription.Any())
        {
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Created, "Creada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.InvalidSchema, "Esquema inválido", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.InvalidData, "Datos inválidos", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Initialized, "Inicializada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Requested, "Solicitada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Declined, "Rechazada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Processed, "Procesada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Notified, "Notificada", typeof(InvoiceProcessingStatus).FullName));
            _dbContext.EnumDescription.Add(new((short)InvoiceProcessingStatus.Cancelled, "Inválida", typeof(InvoiceProcessingStatus).FullName));

            await _dbContext.SaveChangesAsync();
        }
    }
}
