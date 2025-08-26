using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.SharedKernel.Mh.FeNr;

public partial class FeNrv3 : Dte
{
    public override string ToJson()
    => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 3;

    public static string SchemaType
        => "04";

    public static short TypeId
        => 4;

    public static string Desc
        => "Nota de Remisión";

    public static FeNrv3 Deserialize(string json)
        => JsonSerializer.Deserialize<FeNrv3>(json, DefaultJsonSerializerOpts);
}

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
