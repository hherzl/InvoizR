using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class RecepcionDteRequest : BaseModel
{
    public RecepcionDteRequest()
    {
    }

    public RecepcionDteRequest(string ambiente, int? version, string tipoDte, string codigoGeneracion, string documento)
    {
        Ambiente = ambiente;
        IdEnvio = $"{DateTime.Now:yyyyMMddhhmmss}";
        Version = version;
        TipoDte = tipoDte;
        CodigoGeneracion = codigoGeneracion;
        Documento = documento;
    }

    [JsonPropertyName("ambiente")]
    public string Ambiente { get; set; }

    [JsonPropertyName("idEnvio")]
    public string IdEnvio { get; set; }

    [JsonPropertyName("version")]
    public int? Version { get; set; }

    [JsonPropertyName("tipoDte")]
    public string TipoDte { get; set; }

    [JsonPropertyName("codigoGeneracion")]
    public string CodigoGeneracion { get; set; }

    [JsonPropertyName("documento")]
    public string Documento { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}
