using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

public class GetInvoicesBySyncStatusQuerySpec : BaseQuerySpec<InvoiceItemModel>
{
    public GetInvoicesBySyncStatusQuerySpec(short? syncStatusId)
    {
        if (syncStatusId != null)
            Criteria = entity => entity.SyncStatusId == syncStatusId;
    }
}
