using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeCcf;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class Dte03SyncStatusChanger : InvoiceSyncStatusChanger
{
    private readonly ILogger _logger;

    public Dte03SyncStatusChanger(ILogger<Dte03SyncStatusChanger> logger)
        : base(logger)
    {
    }

    protected override bool Init(Invoice invoice)
    {
        FeCcfv3 dte;

        try
        {
            dte = FeCcfv3.Deserialize(invoice.Payload);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, $"There was an error deserializing {invoice.InvoiceNumber} invoice");
            return false;
        }

        dte.Identificacion.Version = (int)invoice.SchemaVersion;
        dte.Identificacion.TipoDte = invoice.SchemaType;
        dte.Identificacion.CodigoGeneracion = invoice.InvoiceGuid;
        dte.Identificacion.NumeroControl = invoice.AuditNumber;

        dte.Resumen.TotalLetras = MoneyToWordsConverter.SpellingNumber(invoice.InvoiceTotal);

        invoice.Payload = dte.ToJson();

        return true;
    }
}
