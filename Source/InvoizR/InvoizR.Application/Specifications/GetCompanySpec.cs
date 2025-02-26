using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetCompanySpec : BaseSpecification<Company>
{
    public GetCompanySpec(short? id)
    {
        Criteria = item => item.Id == id;
    }
}
