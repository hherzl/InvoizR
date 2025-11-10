using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetInvoiceQuerySpec : BaseQuerySpec<Invoice>
{
    public GetInvoiceQuerySpec(long? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
