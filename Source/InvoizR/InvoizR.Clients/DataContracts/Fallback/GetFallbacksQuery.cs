using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts.Fallback;

public record GetFallbacksQuery : IRequest<PagedResponse<FallbackItemModel>>
{
    public GetFallbacksQuery()
    {
        PageNumber = 1;
        PageSize = 10;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
