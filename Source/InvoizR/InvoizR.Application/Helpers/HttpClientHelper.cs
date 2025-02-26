namespace InvoizR.Application.Helpers;

public static class HttpClientHelper
{
    public static HttpClient SetBearerToken(this HttpClient client, string bearer)
    {
        client.DefaultRequestHeaders.Add("Authorization", bearer);

        return client;
    }
}
