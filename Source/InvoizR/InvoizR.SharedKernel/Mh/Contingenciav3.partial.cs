using System.Text.Json;

namespace InvoizR.SharedKernel.Mh.Contingencia;

public partial class Contingenciav3
{
    public override string ToJson()
        => JsonSerializer.Serialize(this, DefaultJsonSerializerOpts);

    public static short Version
        => 3;

    public static string Desc
        => "Evento de Contingencia";

    public static Contingenciav3 Deserialize(string json)
        => JsonSerializer.Deserialize<Contingenciav3>(json, DefaultJsonSerializerOpts);
}
