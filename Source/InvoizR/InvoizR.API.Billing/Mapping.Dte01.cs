using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte01(this WebApplication webApplication)
    {
        webApplication.MapPost("dte01-ow", async (ISender mediator, CreateDte01InvoiceOWCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("dte01-rt", async (ISender mediator, CreateDte01InvoiceRTCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
