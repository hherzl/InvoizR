using System.Text;
using InvoizR.SharedKernel;

namespace InvoizR.Clients;

public static class ContentHelper
{
    public static StringContent Create(string content)
        => new(content, Encoding.Default, Tokens.ApplicationJson);

    public static StringContent CreateEmpty()
        => new("{}", Encoding.Default, Tokens.ApplicationJson);
}
