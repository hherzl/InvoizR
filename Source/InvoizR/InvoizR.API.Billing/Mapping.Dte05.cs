using InvoizR.Clients.DataContracts.Dte05;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte05(this WebApplication webApplication)
    {
        webApplication.MapPost("dte05-rt", async (ISender mediator, CreateDte05RTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte05-ow", async (ISender mediator, CreateDte05OWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
