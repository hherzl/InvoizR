using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh.FeCcf;

namespace InvoizR.SharedKernel.Mh;

public partial class FeCcfv3Received : FeCcfv3
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static FeCcfv3Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeCcfv3Received>(json, DefaultJsonSerializerOpts);
}
