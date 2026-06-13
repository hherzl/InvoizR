using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.QuerySpecs;

public class GetResponsibleByCompanyQuerySpec : BaseQuerySpec<Responsible>
{
    public GetResponsibleByCompanyQuerySpec(short? companyId)
    {
        Criteria = entity => entity.CompanyId == companyId;
    }
}
