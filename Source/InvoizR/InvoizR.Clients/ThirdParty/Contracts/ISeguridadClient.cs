using InvoizR.Clients.ThirdParty.DataContracts;

namespace InvoizR.Clients.ThirdParty.Contracts;

public interface ISeguridadClient
{
    string ServiceName { get; }

    SeguridadClientSettings ClientSettings { get; set; }

    Task<AuthResponse> AuthAsync();
}
