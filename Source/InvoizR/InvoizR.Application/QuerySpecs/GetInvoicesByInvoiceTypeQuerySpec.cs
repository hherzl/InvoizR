using InvoizR.Clients.DataContracts.Invoices;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

public class GetInvoicesByInvoiceTypeQuerySpec : BaseQuerySpec<InvoiceItemModel>
{
    public GetInvoicesByInvoiceTypeQuerySpec(short? invoiceTypeId)
    {
        if (invoiceTypeId != null)
            Criteria = entity => entity.InvoiceTypeId == invoiceTypeId;
    }
}
