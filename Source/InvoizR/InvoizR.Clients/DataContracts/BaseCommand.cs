using System.Text.Json;

namespace InvoizR.Clients.DataContracts;

public record BaseCommand
{
    public static JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { WriteIndented = true };
}
