using InvoizR.Clients.ThirdParty.DataContracts;

namespace InvoizR.Clients.ThirdParty.Contracts;

public interface ISeguridadClient
{
    SeguridadClientSettings ClientSettings { get; }

    Task<AuthResponse> AuthAsync(AuthRequest request);
}
