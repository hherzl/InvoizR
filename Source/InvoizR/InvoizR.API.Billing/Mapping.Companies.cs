using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapCompanies(this WebApplication webApplication)
    {
        webApplication.MapGet("/company", async (ISender mediator, [AsParameters] GetCompaniesQuery request) =>
        {
            var result = await mediator.Send(request);
            return Results.Ok(result);
        });

        webApplication.MapGet("/company/{id}", async (ISender mediator, short id) =>
        {
            var result = await mediator.Send(new GetCompanyQuery(id));
            if (result == null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        webApplication.MapPost("/company", async (ISender mediator, CreateCompanyCommand request) =>
        {
            var result = await mediator.Send(request);
            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
