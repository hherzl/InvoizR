using InvoizR.Application.Common.Contracts;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.Features.Diagnostics.Queries;

public class DiagnosticsSeguridadQueryHandler : IRequestHandler<DiagnosticsSeguridadQuery, DiagnosticsSeguridadResponse>
{
    private readonly IInvoizRDbContext _dbContext;
    private readonly ISeguridadClient _seguridadClient;

    public DiagnosticsSeguridadQueryHandler(IInvoizRDbContext dbContext, ISeguridadClient seguridadClient)
    {
        _dbContext = dbContext;
        _seguridadClient = seguridadClient;
    }

    public async Task<DiagnosticsSeguridadResponse> Handle(DiagnosticsSeguridadQuery request, CancellationToken cancellationToken)
    {
        var company = await _dbContext.GetCompanyAsync(request.CompanyId, ct: cancellationToken);

        var thirdPartyServices = await _dbContext.ThirdPartyServices(company.Environment, includes: true).ToListAsync(cancellationToken);
        if (thirdPartyServices.Count == 0)
            throw new NoThirdPartyServicesException(company.Name, company.Environment);

        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
        _seguridadClient.ClientSettings = thirdPartyServicesParameters.GetByService(_seguridadClient.ServiceName).ToSeguridadClientSettings();

        return new DiagnosticsSeguridadResponse(await _seguridadClient.AuthAsync());
    }
}
