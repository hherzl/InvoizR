using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh.FeNc;

namespace InvoizR.SharedKernel.Mh;

public partial class FeNcv3Received : FeNcv3
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static FeNcv3Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeNcv3Received>(json, DefaultJsonSerializerOpts);
}
