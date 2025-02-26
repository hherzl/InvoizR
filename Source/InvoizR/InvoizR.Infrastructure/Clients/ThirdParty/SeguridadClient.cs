using System.Text.Json;
using InvoizR.Clients.ThirdParty;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Clients.ThirdParty.DataContracts;
using Microsoft.Extensions.Options;

namespace InvoizR.Infrastructure.Clients.ThirdParty;

public class SeguridadClient : MhClient, ISeguridadClient
{
    public SeguridadClient(IOptions<SeguridadClientSettings> options)
        : base()
    {
        ClientSettings = options.Value;
    }

    public SeguridadClientSettings ClientSettings { get; set; }

    public async Task<AuthResponse> AuthAsync(AuthRequest request)
    {
        using var client = CreateHttpClient(ClientSettings.Endpoint);

        client.DefaultRequestHeaders.Add("User-Agent", "mobile");

        var formUrlEncodedContent = new FormUrlEncodedContent
        ([
            new KeyValuePair<string, string>("user", request.User),
            new KeyValuePair<string, string>("pwd", request.Pwd)
        ]);

        var response = await client.PostAsync($"auth", formUrlEncodedContent);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<AuthResponse>(responseContent, DefaultJsonSerializerOpts);
    }
}
