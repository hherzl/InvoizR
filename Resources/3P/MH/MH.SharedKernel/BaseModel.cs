using System.Text.Json;

namespace MH.SharedKernel;

public record BaseModel
{
    public static JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { WriteIndented = true };
}
