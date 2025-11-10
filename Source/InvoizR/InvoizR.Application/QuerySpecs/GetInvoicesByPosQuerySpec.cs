using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.QuerySpecs;

public class GetInvoicesByPosQuerySpec : BaseQuerySpec<InvoiceItemModel>
{
    public GetInvoicesByPosQuerySpec(short? posId)
    {
        if (posId != null)
            Criteria = entity => entity.PosId == posId;
    }
}
