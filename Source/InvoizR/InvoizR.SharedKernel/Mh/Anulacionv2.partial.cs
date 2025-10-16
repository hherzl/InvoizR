using System.Text.Json;

namespace InvoizR.SharedKernel.Mh.Anulacion;

public partial class Anulacionv2
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 2;

    public static string Desc
        => "Invalidación de DTE";

    public static Anulacionv2 Deserialize(string json)
        => JsonSerializer.Deserialize<Anulacionv2>(json, DefaultJsonSerializerOpts);
}
