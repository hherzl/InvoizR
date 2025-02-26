using MH.Mock.Firmador.Models;
using Microsoft.AspNetCore.Mvc;

namespace MH.Mock.Firmador;

public static partial class Mapping
{
    public static WebApplication MapFirmarDocumento(this WebApplication webApplication)
    {
        webApplication.MapGet("firmardocumento/status", () =>
        {
            return Results.Ok();
        });

        webApplication.MapPost("firmardocumento/", async ([FromServices] ILogger logger, [FromBody] FirmarDocumentoRequest<FeFcv1> request) =>
        {
            logger?.LogInformation($"Processing signature for '{request.Nit}'...");

            await Task.Delay(1000);

            return Results.Ok(new FirmarDocumentoResponse("OK", FirmaMocker.Mock()));
        });

        return webApplication;
    }
}
