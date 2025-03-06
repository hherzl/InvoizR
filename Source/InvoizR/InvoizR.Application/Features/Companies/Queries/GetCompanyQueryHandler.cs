using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Companies.Queries;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyDetailsModel>
{
    private readonly IInvoizRDbContext _dbContext;

    public GetCompanyQueryHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CompanyDetailsModel> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.GetCompanyAsync(request.Id, ct: cancellationToken);
        if (entity == null)
            return null;

        var branches = await _dbContext.GetBranchesBy(entity.Id).ToListAsync(cancellationToken);

        var model = new CompanyDetailsModel
        {
            Id = entity.Id,
            Environment = entity.Environment,
            Name = entity.Name,
            Code = entity.Code,
            BusinessName = entity.BusinessName,
            TaxIdNumber = entity.TaxIdNumber,
            TaxRegistrationNumber = entity.TaxRegistrationNumber,
            EconomicActivityId = entity.EconomicActivityId,
            EconomicActivity = entity.EconomicActivity,
            CountryLevelId = entity.CountryLevelId,
            Address = entity.Address,
            Phone = entity.Phone,
            Email = entity.Email,
            Headquarters = entity.Headquarters,
            Branches = branches
        };

        if (entity.Logo != null)
            model.Logo = Convert.ToBase64String(entity.Logo);

        return model;
    }
}
