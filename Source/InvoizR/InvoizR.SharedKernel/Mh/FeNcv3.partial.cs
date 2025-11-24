using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.SharedKernel.Mh.FeNc;

public partial class FeNcv3 : Dte
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 3;

    public static string SchemaType
        => "05";

    public static short TypeId
        => 5;

    public static string Desc
        => "Nota de Crédito";

    public static FeNcv3 Deserialize(string payload)
        => string.IsNullOrEmpty(payload) ? null : JsonSerializer.Deserialize<FeNcv3>(payload, DefaultJsonSerializerOpts);
}

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
