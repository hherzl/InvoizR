using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetResponsiblesQuery : IRequest<ListResponse<ResponsibleItemModel>>
{
    public GetResponsiblesQuery()
    {
    }

    public short? CompanyId { get; set; }
}
