using InvoizR.Application.Helpers;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeFc;

namespace InvoizR.API.Billing.Services;

public class InvoiceHandler
{
    public static bool TryInit(Invoice invoice)
    {
        FeFcv1 dte;

        try
        {
            dte = FeFcv1.Deserialize(invoice.Serialization);
        }
        catch (Exception ex)
        {
            return false;
        }

        dte.Identificacion.Version = (int)invoice.SchemaVersion;
        dte.Identificacion.TipoDte = invoice.SchemaType;
        dte.Identificacion.CodigoGeneracion = invoice.GenerationCode;
        dte.Identificacion.NumeroControl = invoice.ControlNumber;

        dte.Resumen.TotalLetras = MoneyToWordsConverter.SpellingNumber(invoice.InvoiceTotal);

        invoice.Serialization = dte.ToJson();

        return true;
    }
}
