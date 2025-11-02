using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Fallback;

public record GetFallbackQuery : IRequest<SingleResponse<FallbackDetailsModel>>
{
    public GetFallbackQuery(short? id)
    {
        Id = id;
    }

    public short? Id { get; }
}
