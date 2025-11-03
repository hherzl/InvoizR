using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Branches.Queries;

public class GetBranchQueryHandler : IRequestHandler<GetBranchQuery, SingleResponse<BranchDetailsModel>>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetBranchQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SingleResponse<BranchDetailsModel>> Handle(GetBranchQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetBranchAsync(request.Id, ct: cancellationToken);
        if (entity == null)
            return null;

        var posCollection = await _dbContext.GetPosBy(entity.Id).ToListAsync(cancellationToken);

        var model = new BranchDetailsModel
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            Name = entity.Name,
            TaxAuthId = entity.TaxAuthId,
            Address = entity.Address,
            Phone = entity.Phone,
            Email = entity.Email,
            Headquarters = entity.Headquarters,
            ResponsibleId = entity.ResponsibleId,
            Pos = posCollection
        };

        if (entity.Logo != null)
            model.Logo = Convert.ToBase64String(entity.Logo);

        return new(model);
    }
}
