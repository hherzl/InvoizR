using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class RecepcionDteResponse : BaseModel
{
    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static RecepcionDteResponse DeserializeFrom(string json)
        => JsonSerializer.Deserialize<RecepcionDteResponse>(json, DefaultJsonSerializerOpts);

    public RecepcionDteResponse()
    {
    }

    [JsonPropertyName("version")]
    public int? Version { get; set; }

    [JsonPropertyName("ambiente")]
    public string Ambiente { get; set; }

    [JsonPropertyName("versionApp")]
    public int? VersionApp { get; set; }

    [JsonPropertyName("estado")]
    public string Estado { get; set; }

    [JsonPropertyName("codigoGeneracion")]
    public string CodigoGeneracion { get; set; }

    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("fhProcesamiento")]
    public string FhProcesamiento { get; set; }

    [JsonPropertyName("clasificaMsg")]
    public string ClasificaMsg { get; set; }

    [JsonPropertyName("codigoMsg")]
    public string CodigoMsg { get; set; }

    [JsonPropertyName("descripcionMsg")]
    public string DescripcionMsg { get; set; }

    [JsonPropertyName("observaciones")]
    public List<string> Observaciones { get; set; }

    public bool IsSuccessful
        => Estado == "PROCESADO" || DescripcionMsg == "[identificacion.numeroControl] YA EXISTE UN REGISTRO CON ESE VALOR";
}
