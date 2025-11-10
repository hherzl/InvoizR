using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetPosQuerySpec : BaseQuerySpec<Pos>
{
    public GetPosQuerySpec(short? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
