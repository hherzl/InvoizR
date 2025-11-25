using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh.FeNr;

namespace InvoizR.SharedKernel.Mh;

public partial class FeNrv3Received : FeNrv3
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static FeNrv3Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeNrv3Received>(json, DefaultJsonSerializerOpts);
}
