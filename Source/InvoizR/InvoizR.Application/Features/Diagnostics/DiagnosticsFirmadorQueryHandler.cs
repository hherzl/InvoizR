using InvoizR.Application.Common.Contracts;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.Domain.Exceptions;
using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh.FeFc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InvoizR.Application.Features.Diagnostics;

public class DiagnosticsFirmadorQueryHandler : IRequestHandler<DiagnosticsFirmadorQuery, DiagnosticsFirmadorResponse>
{
    private readonly IInvoizRDbContext _dbContext;
    private readonly IFirmadorClient _firmadorClient;

    public DiagnosticsFirmadorQueryHandler(IInvoizRDbContext dbContext, IFirmadorClient firmadorClient)
    {
        _dbContext = dbContext;
        _firmadorClient = firmadorClient;
    }

    public async Task<DiagnosticsFirmadorResponse> Handle(DiagnosticsFirmadorQuery request, CancellationToken cancellationToken)
    {
        var company = await _dbContext.GetCompanyAsync(request.CompanyId, ct: cancellationToken);

        var thirdPartyServices = await _dbContext.GetThirdPartyServices(company.Environment, includes: true).ToListAsync(cancellationToken);
        if (thirdPartyServices.Count == 0)
            throw new NoThirdPartyServicesException(company.Name, company.Environment);

        var thirdPartyServicesParameters = thirdPartyServices.GetThirdPartyClientParameters().ToList();
        var parameters = new
        {
            User = thirdPartyServicesParameters.GetValue("User"),
            PrivateKey = thirdPartyServicesParameters.GetValue("PrivateKey")
        };

        var dte01Request = new CreateDte01Request(thirdPartyServicesParameters, 1, new FeFcv1());
        var firmarDocumentoRequest = new FirmarDocumentoRequest<FeFcv1>(parameters.User, true, parameters.PrivateKey, dte01Request.Dte);
        var firmarDocumentoResponse = await _firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);

        return new DiagnosticsFirmadorResponse(firmarDocumentoResponse);
    }
}
