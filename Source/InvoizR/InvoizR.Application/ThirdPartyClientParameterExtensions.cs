using InvoizR.Clients.ThirdParty;
using InvoizR.SharedKernel;

namespace InvoizR.Application;

public static class ThirdPartyClientParameterExtensions
{
    public static IEnumerable<ThirdPartyClientParameter> GetByService(this IEnumerable<ThirdPartyClientParameter> thirdPartyClientParameters, string service)
            => thirdPartyClientParameters.Where(item => item.Service == service);

    public static SeguridadClientSettings ToSeguridadClientSettings(this IEnumerable<ThirdPartyClientParameter> thirdPartyClientParameters)
    {
        return new()
        {
            Endpoint = thirdPartyClientParameters.GetValue("Endpoint"),
            UserAgent = thirdPartyClientParameters.GetValue("User-Agent"),
            User = thirdPartyClientParameters.GetValue("User"),
            Pwd = thirdPartyClientParameters.GetValue("Pwd")
        };
    }

    public static void AddJwt(this ICollection<ThirdPartyClientParameter> thirdPartyClientParameters, string environment, string token)
        => thirdPartyClientParameters.Add(new(environment, ThirdPatyClientParameterTypes.Jwt, "Token", token));

    public static FirmadorClientSettings ToFirmadorClientSettings(this IEnumerable<ThirdPartyClientParameter> thirdPartyClientParameters)
    {
        return new()
        {
            Endpoint = thirdPartyClientParameters.GetValue("Endpoint"),
            UserAgent = thirdPartyClientParameters.GetValue("User-Agent"),
            PrivateKey = thirdPartyClientParameters.GetValue("PrivateKey")
        };
    }

    public static FesvClientSettings ToFesvClientSettings(this IEnumerable<ThirdPartyClientParameter> thirdPartyClientParameters)
    {
        return new()
        {
            Endpoint = thirdPartyClientParameters.GetValue("Endpoint")
        };
    }
}
