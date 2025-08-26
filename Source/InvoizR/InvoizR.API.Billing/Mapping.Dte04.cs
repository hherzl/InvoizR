using InvoizR.Clients.DataContracts.Dte04;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte04(this WebApplication webApplication)
    {
        webApplication.MapPost("dte04-rt", async (ISender mediator, CreateDte04RTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte04-ow", async (ISender mediator, CreateDte04OWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
