using InvoizR.Clients.DataContracts;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace InvoizR.Application.Features.Diagnostics.Queries;

public class DiagnosticsSeguridadQueryHandler : IRequestHandler<DiagnosticsSeguridadQuery, DiagnosticsSeguridadResponse>
{
    private readonly IConfiguration _configuration;
    private readonly ISeguridadClient _seguridadClient;

    public DiagnosticsSeguridadQueryHandler(IConfiguration configuration, ISeguridadClient seguridadClient)
    {
        _configuration = configuration;
        _seguridadClient = seguridadClient;
    }

    public async Task<DiagnosticsSeguridadResponse> Handle(DiagnosticsSeguridadQuery request, CancellationToken cancellationToken)
    {
        var authRequest = new AuthRequest();
        _configuration.Bind("Clients:Mh", authRequest);

        return new DiagnosticsSeguridadResponse(await _seguridadClient.AuthAsync(authRequest));
    }
}
