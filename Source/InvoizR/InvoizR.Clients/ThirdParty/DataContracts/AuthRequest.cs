using System.Text.Json;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public record AuthRequest
{
    public AuthRequest()
    {
    }

    public AuthRequest(string user, string pwd)
    {
        User = user;
        Pwd = pwd;
    }

    public string User { get; set; }
    public string Pwd { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this);
}
