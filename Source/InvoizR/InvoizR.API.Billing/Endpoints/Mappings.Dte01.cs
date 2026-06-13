using InvoizR.Clients.DataContracts.Dte01;
using MediatR;

namespace InvoizR.API.Billing.Endpoints;

public static partial class Mappings
{
    public static WebApplication MapDte01(this WebApplication webApplication)
    {
        webApplication.MapPost("dte01-ow", async (ISender mediator, CreateDte01OWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte01-rt", async (ISender mediator, CreateDte01RTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
