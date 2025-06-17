using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Reports.Templates.Common;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel.Mh.FeCcf;

namespace InvoizR.Application.Reports.Templates;

public class Dte03TemplateFactory : DteTemplateFactory
{
    private readonly IQrCodeGenerator _qrCodeGenerator;

    public Dte03TemplateFactory(IQrCodeGenerator qrCodeGenerator)
    {
        _qrCodeGenerator = qrCodeGenerator;
    }

    public Dte03TemplateModel Create(Invoice invoice)
    {
        var branch = invoice.Pos.Branch;
        var company = branch.Company;
        var logo = branch.Logo ?? branch.Company.Logo;

        var model = new Dte03TemplateModel
        {
            Qr = Convert.ToBase64String(_qrCodeGenerator.GetBytes(invoice.ExternalUrl ?? EmptyUrl)),
            InvoiceType = FeCcfv3.Desc,
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
            Receiver = new()
            {
                WtId = invoice.CustomerWtId,
                DocumentTypeId = invoice.CustomerDocumentTypeId,
                DocumentNumber = invoice.CustomerDocumentNumber,
                Name = invoice.CustomerName,
                CountryId = invoice.CustomerCountryId,
                CountryLevelId = invoice.CustomerCountryLevelId,
                Address = invoice.CustomerAddress,
                Phone = invoice.CustomerPhone,
                Email = invoice.CustomerEmail,
            },
            SchemaType = invoice.SchemaType,
            SchemaVersion = invoice.SchemaVersion,
            GenerationCode = invoice.GenerationCode,
            ControlNumber = invoice.ControlNumber,
            ProcessingDateTime = invoice.ProcessingDateTime,
            ReceiptStamp = invoice.ReceiptStamp,
            ExternalUrl = invoice.ExternalUrl
        };

        try
        {
            model.Dte = FeCcfv3.Deserialize(invoice.Payload);
        }
        catch (Exception ex)
        {
            model.ErrorMessage = ex.Message;
        }

        return model;
    }
}
