using System.Net.Http.Headers;
using System.Text.Json;

namespace InvoizR.Clients;

public class Client
{
    protected const string APPLICATION_JSON = "application/JSON";
    protected const string USER_AGENT = "User-Agent";

    protected HttpClient _httpClient;

    public Client() { }

    protected void InitClient(string endpoint, string userAgent)
    {
        _httpClient = new HttpClient
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

        _httpClient.DefaultRequestHeaders.Add(USER_AGENT, userAgent);
    }

    protected JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { PropertyNameCaseInsensitive = true, WriteIndented = true };
}
