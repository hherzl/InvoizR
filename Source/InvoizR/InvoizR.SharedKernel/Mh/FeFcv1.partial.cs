using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.SharedKernel.Mh;

public partial class FeFcv1 : Dte
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 1;

    public static string SchemaType
        => "01";

    public static FeFcv1 Deserialize(string json)
        => JsonSerializer.Deserialize<FeFcv1>(json, DefaultJsonSerializerOpts);
}

public partial class FeFcv1Received : FeFcv1
{
    [JsonPropertyName("selloRecibido")]
    public string SelloRecibido { get; set; }

    [JsonPropertyName("firmaElectronica")]
    public string FirmaElectronica { get; set; }

    public static FeFcv1Received DeserializeReceived(string json)
        => JsonSerializer.Deserialize<FeFcv1Received>(json, DefaultJsonSerializerOpts);
}
