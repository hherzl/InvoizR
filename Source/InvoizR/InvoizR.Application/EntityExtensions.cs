using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Entities;
using InvoizR.SharedKernel;

namespace InvoizR.Application;

public static class EntityExtensions
{
    public static IEnumerable<ThirdPartyClientParameter> GetThirdPartyClientParameters(this IEnumerable<ThirdPartyService> thirdPartyServices)
    {
        foreach (var thirdPartyService in thirdPartyServices)
        {
            foreach (var serviceParameter in thirdPartyService.ThirdPartyServiceParameters)
            {
                yield return new(thirdPartyService.EnvironmentId, thirdPartyService.Name, serviceParameter.Category, serviceParameter.Name, serviceParameter.DefaultValue);
            }
        }
    }

    public static CreatedInvoiceResponse ToCreateResponse(this Invoice invoice)
        => new()
        {
            Id = invoice.Id,
            InvoiceTypeId = invoice.InvoiceTypeId,
            SchemaType = invoice.SchemaType,
            SchemaVersion = invoice.SchemaVersion,
            InvoiceGuid = invoice.InvoiceGuid,
            AuditNumber = invoice.AuditNumber,
            ReceiptStamp = invoice.ReceiptStamp
        };
}
