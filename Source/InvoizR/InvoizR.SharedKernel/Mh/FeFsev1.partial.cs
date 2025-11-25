using System.Text.Json;

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

    public static FeFsev1 Deserialize(string payload)
        => string.IsNullOrEmpty(payload) ? null : JsonSerializer.Deserialize<FeFsev1>(payload, DefaultJsonSerializerOpts);
}
