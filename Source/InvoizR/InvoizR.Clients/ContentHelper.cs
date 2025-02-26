using System.Text;

namespace InvoizR.Clients;

public static class ContentHelper
{
    const string ApplicationJson = "application/json";

    public static StringContent Create(string content)
        => new(content, Encoding.Default, ApplicationJson);

    public static StringContent CreateEmpty()
        => new("{}", Encoding.Default, ApplicationJson);
}
