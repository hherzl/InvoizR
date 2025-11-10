using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetResponsibleQuerySpec : BaseQuerySpec<Responsible>
{
    public GetResponsibleQuerySpec(short? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
