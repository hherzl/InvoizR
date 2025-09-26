using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Companies.Commands;

public sealed class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CreatedResponse<short?>>
{
    private readonly IInvoizRDbContext _dbContext;

    public CreateCompanyCommandHandler(IInvoizRDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreatedResponse<short?>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
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
            Headquarters = request.Headquarters,
            NonCustomerEmail = request.NonCustomerEmail
        };

        if (request.HasLogo)
            company.Logo = Convert.FromBase64String(request.Logo);

        _dbContext.Company.Add(company);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new(company.Id);
    }
}
