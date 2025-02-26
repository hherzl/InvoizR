using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapPos(this WebApplication webApplication)
    {
        webApplication.MapPost("/pos", async (ISender mediator, CreatePosCommand request) =>
        {
            var result = await mediator.Send(request);

            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
