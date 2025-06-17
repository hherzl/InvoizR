using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDiagnostics(this WebApplication webApplication)
    {
        webApplication.MapGet("diagnostics/seguridad", async (ISender mediator, [AsParameters] DiagnosticsSeguridadQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        webApplication.MapGet("diagnostics/firmador", async (ISender mediator, [AsParameters] DiagnosticsFirmadorQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        return webApplication;
    }
}
