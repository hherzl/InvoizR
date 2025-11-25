using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh.FeFc;

namespace InvoizR.SharedKernel.Mh;

public partial class FeFcv1Received : FeFcv1
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static FeFcv1Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeFcv1Received>(json, DefaultJsonSerializerOpts);
}
