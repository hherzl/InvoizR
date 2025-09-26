namespace InvoizR.Clients.ThirdParty;

public record FirmadorClientSettings
{
    public string Endpoint { get; set; }
    public string UserAgent { get; set; }
    public string PrivateKey { get; set; }
}
