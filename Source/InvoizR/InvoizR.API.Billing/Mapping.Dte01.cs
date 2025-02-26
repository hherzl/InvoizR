using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDte01(this WebApplication webApplication)
    {
        webApplication.MapPost("dte01", async (ISender mediator, CreateDte01InvoiceCommand request) =>
        {
            var result = await mediator.Send(request);

            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
