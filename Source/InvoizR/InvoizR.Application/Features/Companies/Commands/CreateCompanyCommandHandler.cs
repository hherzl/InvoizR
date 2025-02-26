using InvoizR.Application.Common.Persistence;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Companies.Commands;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public CreateCompanyCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Company
        {
            Environment = request.Environment,
            Name = request.Name,
            Code = request.Code,
            BusinessName = request.BusinessName,
            TaxIdNumber = request.TaxIdNumber,
            TaxRegistrationNumber = request.TaxRegistrationNumber,
            EconomicActivityId = request.EconomicActivityId,
            EconomicActivity = request.EconomicActivity,
            CountryLevelId = request.CountryLevelId,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            Headquarters = request.Headquarters
        };

        if (request.HasLogo)
            entity.Logo = Convert.FromBase64String(request.Logo);

        _dbContext.Company.Add(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(entity.Id);
    }
}
