using System.Net.Http.Headers;
using System.Text.Json;

namespace InvoizR.Infrastructure.Clients.ThirdParty;

public class MhClient
{
    protected const string APPLICATION_JSON = "application/JSON";
    protected const string USER_AGENT = "User-Agent";

    protected static JsonSerializerOptions DefaultJsonSerializerOpts
        => new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

    public virtual HttpClient CreateHttpClient(string endpoint)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(endpoint),
            DefaultRequestHeaders =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue(APPLICATION_JSON)
                }
            }
        };

        client.DefaultRequestHeaders.Add(USER_AGENT, "InvoizR HttpClient");

        return client;
    }

    public string outputPath;
}
