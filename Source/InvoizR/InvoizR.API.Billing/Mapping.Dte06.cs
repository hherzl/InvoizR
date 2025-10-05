using InvoizR.Clients.DataContracts.Dte06;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte06(this WebApplication webApplication)
    {
        webApplication.MapPost("dte06-rt", async (ISender mediator, CreateDte06RTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte06-ow", async (ISender mediator, CreateDte06OWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
