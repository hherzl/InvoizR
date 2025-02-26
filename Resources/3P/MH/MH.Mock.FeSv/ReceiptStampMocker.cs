using System.Text;

namespace MH.Mock.FeSv;

public class ReceiptStampMocker
{
    private static readonly Random Random = new();
    private static readonly string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string Mocker(short length = 36)
    {
        var mock = new StringBuilder();

        mock.Append(DateTime.Now.Year);

        for (var i = 4; i < length; i++)
        {
            mock.Append(Characters[Random.Next(Characters.Length)]);
        }

        return $"{mock}";
    }
}
