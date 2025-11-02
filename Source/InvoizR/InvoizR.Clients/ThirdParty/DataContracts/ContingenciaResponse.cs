using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class ContingenciaResponse : BaseModel
{
    [JsonPropertyName("estado")]
    public string Estado { get; set; }

    [JsonPropertyName("fechaHora")]
    public string FechaHora { get; set; }

    [JsonPropertyName("mensaje")]
    public string Mensaje { get; set; }

    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("observaciones")]
    public List<string> Observaciones { get; set; }

    public bool IsSuccessful
        => Estado == "PROCESADO";

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}
