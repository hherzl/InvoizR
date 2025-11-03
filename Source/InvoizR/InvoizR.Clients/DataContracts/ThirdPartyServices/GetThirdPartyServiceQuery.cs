using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.ThirdPartyServices;

public record GetThirdPartyServiceQuery : IRequest<SingleResponse<ThirdPartyServiceDetailsModel>>
{
    public GetThirdPartyServiceQuery(short id)
    {
        Id = id;
    }

    public short? Id { get; }
}
