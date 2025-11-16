using System.Text.Json;

namespace InvoizR.SharedKernel;

public static class AbstractModel
{
    public static JsonSerializerOptions DefaultJsonSerializerOpts
        => new() { PropertyNameCaseInsensitive = true, WriteIndented = true };
}
