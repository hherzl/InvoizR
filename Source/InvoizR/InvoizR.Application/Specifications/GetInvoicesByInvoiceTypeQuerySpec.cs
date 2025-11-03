using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.Specifications;

public class GetInvoicesByInvoiceTypeQuerySpec : BaseSpecification<InvoiceItemModel>
{
    public GetInvoicesByInvoiceTypeQuerySpec(short? invoiceTypeId)
    {
        if (invoiceTypeId != null)
            Criteria = item => item.InvoiceTypeId == invoiceTypeId;
    }
}
