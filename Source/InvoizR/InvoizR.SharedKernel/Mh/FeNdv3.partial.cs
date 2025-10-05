using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.SharedKernel.Mh.FeNd;

public partial class FeNdv3 : Dte
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 3;

    public static string SchemaType
        => "06";

    public static short TypeId
        => 6;

    public static string Desc
        => "Nota de Débito";

    public static FeNdv3 Deserialize(string json)
        => JsonSerializer.Deserialize<FeNdv3>(json, DefaultJsonSerializerOpts);
}

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
