using System.Text;

namespace MH.Mock.Firmador;

public static class FirmaMocker
{
    public static string Mock()
    {
        char[] chars = ['e', 'y', 'J', 'h', 'b', 'G', 'c', 'i', 'O', 'S', 'U', 'z', 'x', 'M', '9', '.', 'w', '0', 'K', 'I', 'C', 'A', 'a', 'W', 'R', 'l', 'n', 'p', 'Z', 'm', 'j', 'Y', 'N', '2', '4', 'D', 'o', 'g', 'X', 'u', '6', 's', 'Q', 'F', 't', 'v', 'd', 'V', 'k', 'T', 'L', 'B', '3', '1', 'E', '5', 'H', '8', 'P', 'r', '7', '_', 'q', '-', 'f'];
        var length = 2212;
        var output = new StringBuilder();
        for (var i = 0; i < length; i++)
        {
            output.Append(chars[Random.Shared.Next(0, chars.Length - 1)]);
        }

        return output.ToString();
    }
}
