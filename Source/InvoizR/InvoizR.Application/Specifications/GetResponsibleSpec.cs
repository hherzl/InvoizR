using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetResponsibleSpec : BaseSpecification<Responsible>
{
    public GetResponsibleSpec(short? id)
    {
        Criteria = item => item.Id == id;
    }
}
