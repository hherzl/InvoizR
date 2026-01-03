using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Common;
using InvoizR.Domain.Entities;
using MediatR;

namespace InvoizR.Application.Features.Companies;

public sealed class CreateCompanyCommandHandler(IInvoizRDbContext dbContext) : IRequestHandler<CreateCompanyCommand, CreatedResponse<short?>>
{
    public async Task<CreatedResponse<short?>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Company
        {
            Environment = request.Environment,
            Name = request.Name,
            Code = request.Code,
            BusinessName = request.BusinessName,
            TaxIdNumber = request.TaxIdNumber,
            TaxpayerRegistrationNumber = request.TaxpayerRegistrationNumber,
            EconomicActivityId = request.EconomicActivityId,
            EconomicActivity = request.EconomicActivity,
            CountryLevelId = request.CountryLevelId,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            Headquarters = request.Headquarters,
            NonCustomerEmail = request.NonCustomerEmail,
            WebhookNotificationProtocol = request.WebhookNotificationProtocol,
            WebhookNotificationAddress = request.WebhookNotificationAddress,
            WebhookNotificationMisc1 = request.WebhookNotificationMisc1,
            WebhookNotificationMisc2 = request.WebhookNotificationMisc2
        };

        if (request.HasLogo)
            company.Logo = Convert.FromBase64String(request.Logo);

        dbContext.Company.Add(company);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new(company.Id);
    }
}
