using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDiagnostics(this WebApplication webApplication)
    {
        webApplication.MapGet("diagnostics/seguridad/{id}", async (ISender mediator, short id) =>
        {
            var result = await mediator.Send(new DiagnosticsSeguridadQuery(id));
            return Results.Ok(result);
        });

        webApplication.MapGet("diagnostics/firmador/{id}", async (ISender mediator, short id) =>
        {
            var result = await mediator.Send(new DiagnosticsFirmadorQuery(id));
            return Results.Ok(result);
        });

        return webApplication;
    }
}
