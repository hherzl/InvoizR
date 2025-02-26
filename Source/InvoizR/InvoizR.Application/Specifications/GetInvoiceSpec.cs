using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetInvoiceSpec : BaseSpecification<Invoice>
{
    public GetInvoiceSpec(long? id)
    {
        Criteria = item => item.Id == id;
    }
}
