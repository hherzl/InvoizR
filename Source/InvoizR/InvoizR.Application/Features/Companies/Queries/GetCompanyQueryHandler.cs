using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Companies.Queries;

public sealed class GetCompanyQueryHandler(IInvoizRDbContext dbContext) : IRequestHandler<GetCompanyQuery, SingleResponse<CompanyDetailsModel>>
{
    public async Task<SingleResponse<CompanyDetailsModel>> Handle(GetCompanyQuery request, CancellationToken ct)
    {
        var entity = await dbContext.GetCompanyAsync(request.Id, ct: ct);
        if (entity == null)
            return null;

        var branches = await dbContext.GetBranchesBy(entity.Id).ToListAsync(ct);
        var responsibles = await dbContext.GetResponsiblesBy(entity.Id).ToListAsync(ct);

        var model = new CompanyDetailsModel
        {
            Id = entity.Id,
            Environment = entity.Environment,
            Name = entity.Name,
            Code = entity.Code,
            BusinessName = entity.BusinessName,
            TaxIdNumber = entity.TaxIdNumber,
            TaxpayerRegistrationNumber = entity.TaxpayerRegistrationNumber,
            EconomicActivityId = entity.EconomicActivityId,
            EconomicActivity = entity.EconomicActivity,
            CountryLevelId = entity.CountryLevelId,
            Address = entity.Address,
            Phone = entity.Phone,
            Email = entity.Email,
            Headquarters = entity.Headquarters,
            Branches = branches,
            Responsibles = responsibles
        };

        if (entity.Logo != null)
            model.Logo = Convert.ToBase64String(entity.Logo);

        return new(model);
    }
}
