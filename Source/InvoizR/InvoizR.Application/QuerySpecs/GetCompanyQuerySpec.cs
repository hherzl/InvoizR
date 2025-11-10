using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetCompanyQuerySpec : BaseQuerySpec<Company>
{
    public GetCompanyQuerySpec(short? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
