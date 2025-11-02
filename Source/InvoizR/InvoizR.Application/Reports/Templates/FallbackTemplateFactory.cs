using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.Contingencia;

namespace InvoizR.Application.Reports.Templates;

public class FallbackTemplateFactory
{
    public FallbackTemplateFactory()
    {
    }

    public FallbackTemplateModel Create(Fallback invoice)
    {
        var company = invoice.Company;
        var logo = invoice.Company.Logo;

        var model = new FallbackTemplateModel
        {
            InvoiceType = Contingenciav3.Desc,
            Emitter = new()
            {
                BusinessName = company.BusinessName,
                TaxIdNumber = company.TaxIdNumber,
                TaxRegistrationNumber = company.TaxRegistrationNumber,
                EconomicActivityId = company.EconomicActivityId,
                EconomicActivity = company.EconomicActivity,
                Address = company.Address,
                Phone = company.Phone,
                Email = company.Email,
                Logo = Convert.ToBase64String(logo)
            },
            SchemaVersion = Contingenciav3.Version,
            GenerationCode = invoice.FallbackGuid,
            EmitDateTime = invoice.EmitDateTime,
            ReceiptStamp = invoice.ReceiptStamp
        };

        try
        {
            model.Dte = Contingenciav3.Deserialize(invoice.Payload);
        }
        catch (Exception ex)
        {
            model.ErrorMessage = ex.Message;
        }

        return model;
    }
}
