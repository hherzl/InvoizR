using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh.FeNd;

namespace InvoizR.SharedKernel.Mh;

public partial class FeNdv3Received : FeNdv3
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static FeNdv3Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeNdv3Received>(json, DefaultJsonSerializerOpts);
}
