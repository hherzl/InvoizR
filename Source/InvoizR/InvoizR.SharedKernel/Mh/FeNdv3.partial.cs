using System.Text.Json;

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

    public static FeNdv3 Deserialize(string payload)
        => string.IsNullOrEmpty(payload) ? null : JsonSerializer.Deserialize<FeNdv3>(payload, DefaultJsonSerializerOpts);
}
