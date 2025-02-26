using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public record ConsultaDteResponse
{
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
}
