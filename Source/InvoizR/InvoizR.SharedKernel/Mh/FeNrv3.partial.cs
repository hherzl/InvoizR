using System.Text.Json;

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

    public static FeNrv3 Deserialize(string payload)
        => string.IsNullOrEmpty(payload) ? null : JsonSerializer.Deserialize<FeNrv3>(payload, DefaultJsonSerializerOpts);
}
