using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Clients.DataContracts;

public record GetBranchQuery : IRequest<SingleResponse<BranchDetailsModel>>
{
    public GetBranchQuery(short? id)
    {
        Id = id;
    }

    public short? Id { get; }
}
