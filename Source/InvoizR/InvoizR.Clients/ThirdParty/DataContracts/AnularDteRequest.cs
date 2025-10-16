using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class AnularDteRequest : BaseModel
{
    public AnularDteRequest()
    {
    }

    public AnularDteRequest(string ambiente, string documento)
    {
        Ambiente = ambiente;
        IdEnvio = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        Version = 1;
        Documento = documento;
    }

    [JsonPropertyName("ambiente")]
    public string Ambiente { get; set; }

    [JsonPropertyName("idEnvio")]
    public string IdEnvio { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }

    [JsonPropertyName("documento")]
    public string Documento { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}
