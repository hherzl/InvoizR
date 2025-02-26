using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetBranchSpec : BaseSpecification<Branch>
{
    public GetBranchSpec(short? id)
    {
        Criteria = item => item.Id == id;
    }
}
