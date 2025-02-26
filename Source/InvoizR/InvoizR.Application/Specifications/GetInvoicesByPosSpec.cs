using InvoizR.Clients.DataContracts;
using InvoizR.Domain.Common;

namespace InvoizR.Application.Specifications;

public class GetInvoicesByPosSpec : BaseSpecification<InvoiceItemModel>
{
    public GetInvoicesByPosSpec(short? posId)
    {
        if (posId != null)
            Criteria = item => item.PosId == posId;
    }
}
