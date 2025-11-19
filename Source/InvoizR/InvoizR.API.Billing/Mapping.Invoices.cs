using InvoizR.Clients.DataContracts;
using InvoizR.Clients.DataContracts.Invoices;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapInvoices(this WebApplication webApplication)
    {
        webApplication.MapGet("/invoice-viewbag", async (ISender mediator) =>
        {
            var result = await mediator.Send(new GetInvoicesViewBagQuery());
            return Results.Ok(result);
        });

        webApplication.MapGet("/invoice", async (ISender mediator, [AsParameters] GetInvoicesQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        webApplication.MapGet("/invoice/{id}", async (ISender mediator, long id) =>
        {
            var result = await mediator.Send(new GetInvoiceQuery(id));
            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        webApplication.MapPut("/invoice/change-processing-status", async (ISender mediator, [AsParameters] ChangeProcessingStatusCommand request) =>
        {
            var result = await mediator.Send(request);
            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        return webApplication;
    }
}
