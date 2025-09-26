using InvoizR.SharedKernel;

namespace InvoizR.Clients.ThirdParty;

public record SeguridadClientSettings
{
    public string Endpoint { get; set; }
    public string UserAgent { get; set; }
    public string User { get; set; }
    public string Pwd { get; set; }

    public void Load(IEnumerable<ThirdPartyClientParameter> clientParameters)
    {
    }
}
