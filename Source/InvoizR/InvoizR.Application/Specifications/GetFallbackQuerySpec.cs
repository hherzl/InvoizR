using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetFallbackQuerySpec : BaseSpecification<Fallback>
{
    public GetFallbackQuerySpec(short? id)
    {
        Criteria = entity => entity.Id == id;
    }
}
