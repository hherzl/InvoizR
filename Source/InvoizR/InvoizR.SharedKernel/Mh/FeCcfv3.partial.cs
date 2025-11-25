using System.Text.Json;

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

    public static FeCcfv3 Deserialize(string payload)
        => string.IsNullOrEmpty(payload) ? null : JsonSerializer.Deserialize<FeCcfv3>(payload, DefaultJsonSerializerOpts);
}
