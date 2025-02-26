using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetCompanyQuery : IRequest<CompanyDetailsModel>
{
    public GetCompanyQuery(short? id)
    {
        Id = id;
    }

    public short? Id { get; }
}
