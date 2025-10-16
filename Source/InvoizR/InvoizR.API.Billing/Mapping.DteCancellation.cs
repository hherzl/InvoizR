using InvoizR.Clients.DataContracts.Cancellation;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapDteCancellation(this WebApplication webApplication)
    {
        webApplication.MapPost("dte-cancellation", async (ISender mediator, DteCancellationCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        return webApplication;
    }
}
