using Microsoft.AspNetCore.Mvc;

namespace MH.Mock.FeSv;

public static partial class Mapping
{
    public static WebApplication MapContingencia(this WebApplication webApplication)
    {
        webApplication.MapPost("contingencia", async ([FromServices] ILogger logger, [FromBody] ContingenciaRequest request) =>
        {
            var value = new ContingenciaResponse
            {
                Estado = Tokens.Procesado,
                FechaHora = DateTime.Now.ToString(),
                Mensaje = "",
                SelloRecibido = ReceiptStampMocker.Mocker(),
                Observaciones = []
            };

            await Task.Delay(Random.Shared.Next(500, 5000));

            return Results.Ok(value);
        });

        return webApplication;
    }
}
