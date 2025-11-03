using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeFse;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class Dte14SyncStatusChanger : InvoiceSyncStatusChanger
{
    private readonly ILogger logger;

    public Dte14SyncStatusChanger(ILogger<Dte14SyncStatusChanger> logger)
        : base(logger)
    {
    }

    protected override bool Init(Invoice invoice)
    {
        FeFsev1 dte;

        try
        {
            dte = FeFsev1.Deserialize(invoice.Payload);
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, $"There was an error deserializing {invoice.InvoiceNumber} invoice");
            return false;
        }

        dte.Identificacion.Version = (int)invoice.SchemaVersion;
        dte.Identificacion.TipoDte = invoice.SchemaType;
        dte.Identificacion.CodigoGeneracion = invoice.GenerationCode;
        dte.Identificacion.NumeroControl = invoice.AuditNumber;

        dte.Resumen.TotalLetras = MoneyToWordsConverter.SpellingNumber(invoice.InvoiceTotal);

        invoice.Payload = dte.ToJson();

        return true;
    }
}
