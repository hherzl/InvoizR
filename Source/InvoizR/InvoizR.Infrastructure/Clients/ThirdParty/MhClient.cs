using System.Net.Http.Headers;
using InvoizR.SharedKernel;

namespace InvoizR.Infrastructure.Clients.ThirdParty;

public class MhClient
{
    public virtual HttpClient CreateHttpClient(string endpoint, string userAgent = "mobile")
    {
        var client = new HttpClient
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

        client.DefaultRequestHeaders.Add(Tokens.UserAgent, userAgent);

        return client;
    }

    public string outputPath;
}
