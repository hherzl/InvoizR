using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeCcf;
using Microsoft.Extensions.Logging;

namespace InvoizR.Application.Services;

public sealed class Dte03ProcessingStatusChanger : DteProcessingStatusChanger
{
    private readonly ILogger _logger;

    public Dte03ProcessingStatusChanger(ILogger<Dte03ProcessingStatusChanger> logger)
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
        dte.Identificacion.CodigoGeneracion = invoice.GenerationCode;
        dte.Identificacion.NumeroControl = invoice.ControlNumber;

        dte.Resumen.TotalLetras = MoneyToWordsConverter.SpellingNumber(invoice.InvoiceTotal);

        invoice.Payload = dte.ToJson();

        return true;
    }
}
