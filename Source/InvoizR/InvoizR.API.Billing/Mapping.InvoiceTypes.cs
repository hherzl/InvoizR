using InvoizR.Clients.DataContracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapInvoiceTypes(this WebApplication webApplication)
    {
        webApplication.MapGet("/invoice-type", async (ISender mediator, [AsParameters] GetInvoiceTypesQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        webApplication.MapPost("/invoice-type/{id}/set-as-current", async ([FromServices] ISender mediator, short id) =>
        {
            var result = await mediator.Send(new SetInvoiceTypeAsCurrentCommand(id));
            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        return webApplication;
    }
}
