using Microsoft.AspNetCore.Mvc;

namespace MH.Mock.FeSv;

public static partial class Mapping
{
    public const int VersionApp = 2;

    public static WebApplication MapReceptionDte(this WebApplication webApplication)
    {
        webApplication.MapPost("recepciondte", async ([FromServices] ILogger logger, [FromBody] RecepcionDteRequest request) =>
        {
            var value = new RecepcionDteResponse
            {
                Version = request.Version,
                Ambiente = request.Ambiente,
                VersionApp = VersionApp,
                Estado = Tokens.Procesado,
                CodigoGeneracion = request.CodigoGeneracion,
                SelloRecibido = ReceiptStampMocker.Mocker(),
                FhProcesamiento = DateTime.Now.ToString(),
                ClasificaMsg = null,
                CodigoMsg = "001",
                DescripcionMsg = "",
                Observaciones = []
            };

            await Task.Delay(Random.Shared.Next(500, 5000));

            return Results.Ok(value);
        });

        webApplication.MapPost("recepciondte-error", async ([FromServices] ILogger logger, [FromBody] RecepcionDteRequest request) =>
        {
            var value = new RecepcionDteResponse
            {
                Version = request.Version,
                Ambiente = request.Ambiente,
                VersionApp = VersionApp,
                Estado = Tokens.Rechazado,
                CodigoGeneracion = request.CodigoGeneracion,
                SelloRecibido = "",
                FhProcesamiento = DateTime.Now.ToString(),
                ClasificaMsg = null,
                CodigoMsg = "100",
                DescripcionMsg = "DOCUMENTO NO CUMPLE ESQUEMA JSON",
                Observaciones =
                [
                    "Campo #/emisor/nit NIT CONTRIBUYENTE NO EXISTE"
                ]
            };

            await Task.Delay(Random.Shared.Next(500, 2500));

            return Results.Ok(value);
        });

        return webApplication;
    }
}
