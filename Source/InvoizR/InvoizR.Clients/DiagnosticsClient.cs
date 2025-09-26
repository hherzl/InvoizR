using System.Text.Json;
using InvoizR.Clients.Contracts;
using InvoizR.Clients.DataContracts;
using Microsoft.Extensions.Options;

namespace InvoizR.Clients;

public class DiagnosticsClient : Client, IDiagnosticsClient
{
    public DiagnosticsClient(IOptions<DiagnosticsClientSettings> options)
        : base()
    {
        ClientSettings = options.Value;

        InitClient(ClientSettings.Endpoint, "Diagnostics HttpClient");
    }

    public DiagnosticsClientSettings ClientSettings { get; }

    public async Task<DiagnosticsSeguridadResponse> DiagnosticsSeguridadAsync(short id)
    {
        var response = await _httpClient.GetAsync($"diagnostics/seguridad/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<DiagnosticsSeguridadResponse>(responseContent, DefaultJsonSerializerOpts);
    }

    public async Task<DiagnosticsFirmadorResponse> DiagnosticsFirmadorAsync(short id)
    {
        var response = await _httpClient.GetAsync($"diagnostics/firmador/{id}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<DiagnosticsFirmadorResponse>(responseContent, DefaultJsonSerializerOpts);
    }
}
