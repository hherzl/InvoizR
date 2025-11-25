using System.Text.Json;
using System.Text.Json.Serialization;
using InvoizR.SharedKernel.Mh.FeFse;

namespace InvoizR.SharedKernel.Mh;

public partial class FeFsev1Received : FeFsev1
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static FeFsev1Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeFsev1Received>(json, DefaultJsonSerializerOpts);
}
