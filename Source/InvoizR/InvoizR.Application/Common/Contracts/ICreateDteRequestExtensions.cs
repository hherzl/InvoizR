using InvoizR.SharedKernel;
using InvoizR.SharedKernel.Mh;

namespace InvoizR.Application.Common.Contracts;

public static class ICreateDteRequestExtensions
{
    public static string GetUser<TDte>(this ICreateDteRequest<TDte> request) where TDte : Dte
        => request.ThirdPartyClientParameters.GetValue("User");

    public static string GetPrivateKey<TDte>(this ICreateDteRequest<TDte> request) where TDte : Dte
        => request.ThirdPartyClientParameters.GetValue("PrivateKey");

    public static string GetToken<TDte>(this ICreateDteRequest<TDte> request) where TDte : Dte
        => request.ThirdPartyClientParameters.GetValue("Token");

    public static string GetPublicQuery<TDte>(this ICreateDteRequest<TDte> request) where TDte : Dte
        => request.ThirdPartyClientParameters.GetValue("PublicQuery");
}
