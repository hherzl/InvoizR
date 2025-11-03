using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;

namespace InvoizR.Application.Features.Pos.Commands;

public class CreatePosCommandHandler : IRequestHandler<CreatePosCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public CreatePosCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(CreatePosCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Pos
        {
            BranchId = request.BranchId,
            Name = request.Name,
            Code = request.Code,
            TaxAuthId = request.TaxAuthId,
            Description = request.Description
        };

        _dbContext.Pos.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(entity.Id);
    }
}
