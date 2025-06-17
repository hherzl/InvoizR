using InvoizR.Application.Common;
using InvoizR.Application.Services.Models;
using InvoizR.Clients.DataContracts;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.SharedKernel.Mh.FeFc;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace InvoizR.Application.Features.Diagnostics.Queries;

public class DiagnosticsFirmadorQueryHandler : IRequestHandler<DiagnosticsFirmadorQuery, DiagnosticsFirmadorResponse>
{
    private readonly IConfiguration _configuration;
    private readonly ISeguridadClient _seguridadClient;
    private readonly IFirmadorClient _firmadorClient;

    public DiagnosticsFirmadorQueryHandler(IConfiguration configuration, ISeguridadClient seguridadClient, IFirmadorClient firmadorClient)
    {
        _configuration = configuration;
        _seguridadClient = seguridadClient;
        _firmadorClient = firmadorClient;
    }

    public async Task<DiagnosticsFirmadorResponse> Handle(DiagnosticsFirmadorQuery request, CancellationToken cancellationToken)
    {
        var mhSettings = new MhSettings();
        _configuration.Bind("Clients:Mh", mhSettings);

        var processingSettings = new ProcessingSettings();
        _configuration.Bind("ProcessingSettings", processingSettings);

        var authRequest = new AuthRequest();
        _configuration.Bind("Clients:Mh", authRequest);

        var authResponse = await _seguridadClient.AuthAsync(authRequest);

        var dte01Request = CreateDte01Request.Create(mhSettings, processingSettings, authResponse.Body.Token, 1, new FeFcv1().ToJson());
        var firmarDocumentoRequest = new FirmarDocumentoRequest<FeFcv1>(mhSettings.User, true, mhSettings.PrivateKey, dte01Request.Dte);
        var firmarDocumentoResponse = await _firmadorClient.FirmarDocumentoAsync(firmarDocumentoRequest);

        return new DiagnosticsFirmadorResponse(firmarDocumentoResponse);
    }
}
