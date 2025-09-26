using System.Text.Json;
using InvoizR.Clients;
using InvoizR.Clients.ThirdParty;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.SharedKernel.Mh;
using Microsoft.Extensions.Options;

namespace InvoizR.Infrastructure.Clients.ThirdParty;

public class FirmadorClient : MhClient, IFirmadorClient
{
    public FirmadorClient(IOptions<FirmadorClientSettings> options)
        : base()
    {
        ClientSettings = options.Value;
    }

    public string ServiceName
        => "Firmador";

    public FirmadorClientSettings ClientSettings { get; set; }

    public async Task<string> StatusAsync()
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        var response = await client.GetAsync($"firmardocumento/status");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<FirmarDocumentoResponse> FirmarDocumentoAsync<TDte>(FirmarDocumentoRequest<TDte> request) where TDte : Dte
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        var response = await client.PostAsync($"firmardocumento/", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<FirmarDocumentoResponse>(responseContent, DefaultJsonSerializerOpts);
    }
}
