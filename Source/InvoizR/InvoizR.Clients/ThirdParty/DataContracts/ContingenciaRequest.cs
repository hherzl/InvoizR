using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.Clients.ThirdParty.DataContracts;

public class ContingenciaRequest : BaseModel
{
    public ContingenciaRequest()
    {
    }

    public ContingenciaRequest(string nit, string documento)
    {
        Nit = nit;
        Documento = documento;
    }

    [JsonPropertyName("nit")]
    public string Nit { get; set; }

    [JsonPropertyName("documento")]
    public string Documento { get; set; }

    public string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);
}
