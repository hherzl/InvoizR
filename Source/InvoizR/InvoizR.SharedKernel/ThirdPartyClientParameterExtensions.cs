namespace InvoizR.SharedKernel;

public static class ThirdPartyClientParameterExtensions
{
    public static IEnumerable<ThirdPartyClientParameter> GetHeaders(this IEnumerable<ThirdPartyClientParameter> parameters)
        => parameters.Where(item => item.Type == ThirdPatyClientParameterTypes.Header);

    public static IEnumerable<ThirdPartyClientParameter> GetBody(this IEnumerable<ThirdPartyClientParameter> parameters)
        => parameters.Where(item => item.Type == ThirdPatyClientParameterTypes.Body);

    public static ThirdPartyClientParameter Get(this IEnumerable<ThirdPartyClientParameter> parameters, string name)
        => parameters.First(item => item.Name == name);

    public static string GetValue(this IEnumerable<ThirdPartyClientParameter> parameters, string name)
        => parameters.FirstOrDefault(item => item.Name == name)?.Value;
}
