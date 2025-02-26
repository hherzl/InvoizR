using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class ContingenciaRequest : BaseModel
{
    [JsonPropertyName("nit")]
    public string Nit { get; set; }

    [JsonPropertyName("documento")]
    public string Documento { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}
