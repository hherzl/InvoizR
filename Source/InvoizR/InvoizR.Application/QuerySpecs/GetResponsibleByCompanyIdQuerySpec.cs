using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetResponsibleByCompanyIdQuerySpec : BaseQuerySpec<Responsible>
{
    public GetResponsibleByCompanyIdQuerySpec(short? companyId)
    {
        Criteria = entity => entity.CompanyId == companyId;
    }
}
