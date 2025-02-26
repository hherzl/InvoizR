using System.Text.Json;

namespace InvoizR.SharedKernel.Mh;

public class Dte
{
    public static JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { PropertyNameCaseInsensitive = true, WriteIndented = true };

    public virtual string ToJson()
        => JsonSerializer.Serialize(new { }, DefaultJsonSerializerOpts);
}
