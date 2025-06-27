using System.Text.Json;
using System.Text.Json.Serialization;

namespace InvoizR.SharedKernel.Mh.FeFse;

public partial class FeFsev1 : Dte
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 14;

    public static string SchemaType
        => "14";

    public static short TypeId
        => 14;

    public static string Desc
        => "Factura Sujeto Excluido";

    public static FeFsev1 Deserialize(string json)
        => JsonSerializer.Deserialize<FeFsev1>(json, DefaultJsonSerializerOpts);
}

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
