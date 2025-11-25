using System.Text.Json;

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
