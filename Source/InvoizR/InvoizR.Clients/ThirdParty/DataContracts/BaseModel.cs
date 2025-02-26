using System.Text.Json;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public abstract class BaseModel
{
    protected static JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { PropertyNameCaseInsensitive = true, WriteIndented = true };
}
