using System.Text.Json;
using InvoizR.Application.Helpers;
using InvoizR.Clients;
using InvoizR.Clients.ThirdParty;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using Microsoft.Extensions.Options;

namespace InvoizR.Infrastructure.Clients.ThirdParty;

public class FeSvClient : MhClient, IFeSvClient
{
    public FeSvClient(IOptions<FesvClientSettings> options)
        : base()
    {
        ClientSettings = options.Value;
    }

    public string ServiceName
        => "FE SV";

    public FesvClientSettings ClientSettings { get; set; }

    public string Jwt { get; set; }

    public async Task<RecepcionDteResponse> RecepcionDteAsync(RecepcionDteRequest request)
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        client.SetBearerToken(Jwt);

        var response = await client.PostAsync($"recepciondte", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<RecepcionDteResponse>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<ConsultaDteResponse> ConsultaDteAsync(ConsultaDteRequest request)
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        client.SetBearerToken(Jwt);

        var response = await client.PostAsync($"consultadte", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ConsultaDteResponse>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<ContingenciaResponse> ContingenciaAsync(ContingenciaRequest request)
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        client.SetBearerToken(Jwt);

        var response = await client.PostAsync($"contingencia", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<ContingenciaResponse>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<RecepcionDteResponse> AnularDteAsync(AnularDteRequest request)
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        client.SetBearerToken(Jwt);

        var response = await client.PostAsync($"anulardte", ContentHelper.Create(request.ToJson()));
        var responseContent = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<RecepcionDteResponse>(responseContent, DefaultJsonSerializerOpts);
    }
}
