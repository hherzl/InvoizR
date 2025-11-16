using System.Net.Http.Headers;
using System.Text.Json;
using InvoizR.SharedKernel;

namespace InvoizR.Clients;

public class Client
{
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
                    new MediaTypeWithQualityHeaderValue(Tokens.ApplicationJson)
                }
            }
        };

        _httpClient.DefaultRequestHeaders.Add(Tokens.UserAgent, userAgent);
    }

    protected JsonSerializerOptions DefaultJsonSerializerOpts
        => AbstractModel.DefaultJsonSerializerOpts;
}
