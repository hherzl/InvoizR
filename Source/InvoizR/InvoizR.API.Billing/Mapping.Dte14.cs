using InvoizR.Clients.DataContracts.Dte14;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte14(this WebApplication webApplication)
    {
        webApplication.MapPost("dte14-ow", async (ISender mediator, CreateDte14OWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte14-rt", async (ISender mediator, CreateDte14RTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
