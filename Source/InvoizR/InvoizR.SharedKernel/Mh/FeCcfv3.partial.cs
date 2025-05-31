using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.SharedKernel.Mh.FeCcf;

public partial class FeCcfv3 : Dte
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 3;

    public static string SchemaType
        => "03";

    public static short TypeId
        => 3;

    public static string Desc
        => "Comprobante de Crédito Fiscal";

    public static FeCcfv3 Deserialize(string json)
        => JsonSerializer.Deserialize<FeCcfv3>(json, DefaultJsonSerializerOpts);
}

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
