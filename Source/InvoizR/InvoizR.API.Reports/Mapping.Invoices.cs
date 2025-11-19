using InvoizR.Clients.DataContracts.Invoices;
using MediatR;

namespace InvoizR.API.Reports;

public static class Mapping
{
    public static WebApplication MapInvoice(this WebApplication webApplication)
    {
        webApplication.MapGet("/invoice/{id}/qr", async (ISender mediator, long id) =>
        {
            var result = await mediator.Send(new GetInvoiceQrQuery(id));
            if (result == null)
                return Results.NotFound();

            return Results.File(result.Qr, result.ContentType, result.AuditNumber);
        });

        return webApplication;
    }
}
