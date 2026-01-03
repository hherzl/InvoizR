using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Branches;

public sealed class GetBranchQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetBranchQuery, SingleResponse<BranchDetailsModel>>
{
    public async Task<SingleResponse<BranchDetailsModel>> Handle(GetBranchQuery request, CancellationToken ct)
    {
        var entity = await dbContext.GetBranchAsync(request.Id, ct: ct);
        if (entity == null)
            return null;

        var pointsOfSales = await dbContext.GetPosBy(entity.Id).ToListAsync(ct);

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
            Pos = pointsOfSales
        };

        if (entity.Logo != null)
            model.Logo = Convert.ToBase64String(entity.Logo);

        return new(model);
    }
}
