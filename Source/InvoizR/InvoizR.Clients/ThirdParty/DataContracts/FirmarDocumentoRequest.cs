using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class FirmarDocumentoRequest<TDte> : BaseModel where TDte : Dte
{
    public FirmarDocumentoRequest(string nit, bool activo, string passwordPri, TDte dteJson)
    {
        Nit = nit;
        Activo = activo;
        PasswordPri = passwordPri;
        DteJson = dteJson;
    }

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
