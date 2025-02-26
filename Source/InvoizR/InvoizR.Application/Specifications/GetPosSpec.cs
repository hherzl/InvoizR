using InvoizR.Domain.Common;
using InvoizR.Domain.Entities;

namespace InvoizR.Application.Specifications;

public class GetPosSpec : BaseSpecification<Pos>
{
    public GetPosSpec(short? id)
    {
        Criteria = item => item.Id == id;
    }
}
