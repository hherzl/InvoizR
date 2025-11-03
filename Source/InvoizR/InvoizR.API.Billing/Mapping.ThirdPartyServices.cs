using InvoizR.Clients.DataContracts.ThirdPartyServices;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapThirdPartyServices(this WebApplication webApplication)
    {
        webApplication.MapGet("/third-party-service", async (ISender mediator, [AsParameters] GetThirdPartyServicesQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        webApplication.MapGet("/third-party-service/{id}", async (ISender mediator, short id) =>
        {
            var result = await mediator.Send(new GetThirdPartyServiceQuery(id));
            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        return webApplication;
    }
}
