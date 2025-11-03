using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.Specifications;

public class GetInvoicesBySyncStatusQuerySpec : BaseSpecification<InvoiceItemModel>
{
    public GetInvoicesBySyncStatusQuerySpec(short? syncStatusId)
    {
        if (syncStatusId != null)
            Criteria = item => item.SyncStatusId == syncStatusId;
    }
}
