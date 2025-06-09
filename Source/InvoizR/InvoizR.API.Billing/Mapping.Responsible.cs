using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapResponsibles(this WebApplication webApplication)
    {
        webApplication.MapGet("/responsible", async (ISender mediator, [AsParameters] GetResponsiblesQuery request) =>
        {
            var result = await mediator.Send(request);

            return Results.Ok(result);
        });

        webApplication.MapPost("/responsible", async (ISender mediator, CreateResponsibleCommand request) =>
        {
            var result = await mediator.Send(request);

            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
