using System.Text;

namespace MH.Mock.Seguridad;

public static class TokenMocker
{
    public static string Mock()
    {
        char[] chars = ['e', 'y', 'J', 'h', 'b', 'G', 'c', 'i', 'O', 'I', 'U', 'z', 'x', 'M', '9', '.', 'd', 'W', 'w', 'N', 'j', 'E', '0', 'T', 'Y', 'D', 'A', 'S', 's', 'm', 'F', '1', 'v', 'l', 'a', 'V', 'p', 'R', 'L', 'C', 'X', 'Q', '8', 'o', '3', 'H', 'g', 'q', '7', '4', 'Z', 'n', '6', '5', 'K', '_', 'r', 'P', '-', '2', 't', 'u',];
        var length = 244;
        var output = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            output.Append(chars[Random.Shared.Next(0, chars.Length - 1)]);
        }

        return output.ToString();
    }
}
