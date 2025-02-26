using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class ConsultaDteRequest : BaseModel
{
    [JsonPropertyName("observaciones")]
    public string NitEmisor { get; set; }

    [JsonPropertyName("TDte")]
    public string TDte { get; set; }

    [JsonPropertyName("codigoGeneracion")]
    public string CodigoGeneracion { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}
