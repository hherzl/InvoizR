using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetCurrentFallbackQuerySpec : BaseSpecification<Fallback>
{
    public GetCurrentFallbackQuerySpec(short? companyId)
    {
        Criteria = entity => entity.CompanyId == companyId && entity.Enable == true;
    }
}
