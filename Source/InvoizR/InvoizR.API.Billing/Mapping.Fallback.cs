using InvoizR.Clients.DataContracts.Fallback;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapFallback(this WebApplication webApplication)
    {
        webApplication.MapGet("/fallback", async (ISender mediator, [AsParameters] GetFallbacksQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        webApplication.MapGet("/fallback/{id}", async (ISender mediator, short id) =>
        {
            var result = await mediator.Send(new GetFallbackQuery(id));
            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        webApplication.MapPost("/fallback", async (ISender mediator, CreateFallbackCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPut("/fallback/{id}/process", async (ISender mediator, short id) =>
        {
            var result = await mediator.Send(new ProcessFallbackCommand(id));
            return Results.Ok(result);
        });

        return webApplication;
    }
}
