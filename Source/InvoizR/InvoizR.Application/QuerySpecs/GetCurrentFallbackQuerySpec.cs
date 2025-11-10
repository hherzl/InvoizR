using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetCurrentFallbackQuerySpec : BaseQuerySpec<Fallback>
{
    public GetCurrentFallbackQuerySpec(short? companyId)
    {
        Criteria = entity => entity.CompanyId == companyId && entity.Enable == true;
    }
}
