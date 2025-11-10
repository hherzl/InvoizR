using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetBranchQuerySpec : BaseQuerySpec<Branch>
{
    public GetBranchQuerySpec(short? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
