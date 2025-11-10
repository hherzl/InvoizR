using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetFallbackQuerySpec : BaseQuerySpec<Fallback>
{
    public GetFallbackQuerySpec(short? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
