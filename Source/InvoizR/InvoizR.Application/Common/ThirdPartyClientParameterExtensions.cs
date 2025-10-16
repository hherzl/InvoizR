using InvoizR.SharedKernel;

namespace InvoizR.Application.Common;

public static class ThirdPartyClientParameterExtensions
{
    public static string GetUser(this IEnumerable<ThirdPartyClientParameter> clientParameters)
        => clientParameters.GetValue("User");

    public static string GetPrivateKey(this IEnumerable<ThirdPartyClientParameter> clientParameters)
        => clientParameters.GetValue("PrivateKey");

    public static string GetToken(this IEnumerable<ThirdPartyClientParameter> clientParameters)
        => clientParameters.GetValue("Token");

    public static string GetPublicQuery(this IEnumerable<ThirdPartyClientParameter> clientParameters)
        => clientParameters.GetValue("PublicQuery");
}
