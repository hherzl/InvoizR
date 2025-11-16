using System.Text.Json;
using InvoizR.Clients.ThirdParty;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using InvoizR.SharedKernel;
using Microsoft.Extensions.Options;

namespace InvoizR.Infrastructure.Clients.ThirdParty;

public class SeguridadClient : MhClient, ISeguridadClient
{
    public SeguridadClient(IOptions<SeguridadClientSettings> options) : base() { }

    public string ServiceName
        => "Seguridad";

    public SeguridadClientSettings ClientSettings { get; set; }

    public async Task<AuthResponse> AuthAsync()
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint, ClientSettings.UserAgent);

        var formUrlEncodedContent = new FormUrlEncodedContent
        ([
            new KeyValuePair<string, string>("user", ClientSettings.User),
            new KeyValuePair<string, string>("pwd", ClientSettings.Pwd)
        ]);

        var response = await client.PostAsync($"auth", formUrlEncodedContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuthResponse>(responseContent, AbstractModel.DefaultJsonSerializerOpts);
    }
}
