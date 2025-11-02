using Microsoft.AspNetCore.Mvc;

namespace MH.Mock.FeSv;

public static partial class Mapping
{
    public static WebApplication MapAnularDte(this WebApplication webApplication)
    {
        webApplication.MapPost("anulardte", async ([FromServices] ILogger logger, [FromBody] AnularDteRequest request) =>
        {
            var value = new RecepcionDteResponse
            {
                Version = request.Version,
                Ambiente = request.Ambiente,
                VersionApp = VersionApp,
                Estado = Tokens.Procesado,
                CodigoGeneracion = Guid.NewGuid().ToString().ToUpper(),
                SelloRecibido = ReceiptStampMocker.Mocker(),
                FhProcesamiento = DateTime.Now.ToString(),
                ClasificaMsg = null,
                CodigoMsg = "001",
                DescripcionMsg = "Invalidación Recibida y Procesada",
                Observaciones = []
            };

            await Task.Delay(Random.Shared.Next(500, 5000));

            return Results.Ok(value);
        });

        return webApplication;
    }
}
