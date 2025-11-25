using System.Text.Json;

namespace InvoizR.SharedKernel.Mh.FeFc;

public partial class FeFcv1 : Dte
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 1;

    public static string SchemaType
        => "01";

    public static short TypeId
        => 1;

    public static string Desc
        => "Consumidor Final";

    public static FeFcv1 Deserialize(string payload)
        => string.IsNullOrEmpty(payload) ? null : JsonSerializer.Deserialize<FeFcv1>(payload, DefaultJsonSerializerOpts);
}
