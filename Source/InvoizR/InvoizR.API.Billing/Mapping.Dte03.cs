using InvoizR.Clients.DataContracts.Dte03;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte03(this WebApplication webApplication)
    {
        webApplication.MapPost("dte03-ow", async (ISender mediator, CreateDte03OWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte03-rt", async (ISender mediator, CreateDte03RTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
