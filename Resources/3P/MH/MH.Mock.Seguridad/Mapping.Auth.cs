using MH.SharedKernel;
using Microsoft.AspNetCore.Mvc;

namespace MH.Mock.Seguridad;

public static class Mapping
{
    public static WebApplication MapAuth(this WebApplication webApplication)
    {
        webApplication.MapPost("auth", ([FromServices] ILogger logger, [FromForm] string user, [FromForm] string pwd) =>
        {
            var request = new
            {
                User = user,
                Pwd = pwd
            };

            logger?.LogInformation($"Processing token for {request?.User}...");

            if (!MhDb.TaxPayers.Any(item => item.Id == request.User))
                return Results.Unauthorized();

            var response = new AuthResponse
            {
                Status = "OK",
                Body = new()
                {
                    User = request.User,
                    Token = $"Bearer {TokenMocker.Mock()}",
                    Rol = new()
                    {
                        Nombre = "Usuario",
                        Codigo = "ROLE_USER",
                    },
                    Roles = ["ROLE_USER"],
                    TokenType = "Bearer"
                }
            };

            logger?.LogInformation($" Processed token for {request?.User}: {response.Body.Token.Length}");

            return Results.Ok(response);
        }).DisableAntiforgery();

        return webApplication;
    }
}
