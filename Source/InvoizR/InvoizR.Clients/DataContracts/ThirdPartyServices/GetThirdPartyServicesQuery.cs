using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.ThirdPartyServices;

public record GetThirdPartyServicesQuery : IRequest<ListResponse<ThirdPartyServiceItemModel>>
{
}
