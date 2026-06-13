using InvoizR.Clients.DataContracts.Reports;
using MediatR;

namespace InvoizR.API.Reports.Endpoints;

public static partial class Mappings
{
    public static WebApplication MapReports(this WebApplication webApplication)
    {
        webApplication.MapGet("/reports/invoices", async (ISender mediator, [AsParameters] GetInvoicesReportQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        return webApplication;
    }
}
