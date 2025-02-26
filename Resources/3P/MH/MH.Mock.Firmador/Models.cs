using System.Text.Json.Serialization;
using System.Text.Json;
using MH.SharedKernel;

namespace MH.Mock.Firmador;

public record FirmarDocumentoRequest<TDte> : BaseModel where TDte : Dte
{
    [JsonPropertyName("nit")]
    public string Nit { get; set; }

    [JsonPropertyName("activo")]
    public bool Activo { get; set; }

    [JsonPropertyName("passwordPri")]
    public string PasswordPri { get; set; }

    [JsonPropertyName("dteJson")]
    public TDte DteJson { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}

public record FirmarDocumentoResponse
{
    public FirmarDocumentoResponse(string status, string body)
    {
        Status = status;
        Body = body;
    }

    public string Status { get; set; }
    public string Body { get; set; }
}
