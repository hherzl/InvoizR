using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class FirmarDocumentoResponse : BaseModel
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public override string ToString()
        => ToJson();
}
