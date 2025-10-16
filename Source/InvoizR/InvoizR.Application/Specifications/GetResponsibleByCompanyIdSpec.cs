using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetResponsibleByCompanyIdSpec : BaseSpecification<Responsible>
{
    public GetResponsibleByCompanyIdSpec(short? companyId)
    {
        Criteria = item => item.CompanyId == companyId;
    }
}
