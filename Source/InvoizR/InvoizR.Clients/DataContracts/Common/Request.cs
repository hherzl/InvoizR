using System.Text.Json;

namespace InvoizR.Clients.DataContracts.Common;

public record Request
{
    protected JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { WriteIndented = true };
}
