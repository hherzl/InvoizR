using System.Text.Json;
using System.Text.Json.Serialization;
using MH.SharedKernel;

namespace MH.Mock.FeSv;

public record RecepcionDteRequest : BaseModel
{
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

public record RecepcionDteResponse : BaseModel
{
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
    {
        get
        {
            var messages = new List<string>()
            {
                Tokens.CodigoGeneracionExistente,
                Tokens.NumeroControlExistente,
                Tokens.Procesado
            };

            return messages.Any(item => item == Estado);
        }
    }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}

public record AnularDteRequest : BaseModel
{
    public AnularDteRequest()
    {
    }

    public AnularDteRequest(string ambiente, string idEnvio, int version, string documento)
    {
        Ambiente = ambiente;
        IdEnvio = idEnvio;
        Version = version;
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

public static class Tokens
{
    public const string CodigoGeneracionExistente = "[identificacion.codigoGeneracion] YA EXISTE UN REGISTRO CON ESE VALOR";
    public const string NumeroControlExistente = "[identificacion.numeroControl] YA EXISTE UN REGISTRO CON ESE VALOR";
    public const string Procesado = "PROCESADO";
}
