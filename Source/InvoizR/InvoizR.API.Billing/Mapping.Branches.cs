﻿using InvoizR.Clients.DataContracts;
using MediatR;

namespace InvoizR.API.Billing;

public static partial class Mapping
{
    public static WebApplication MapBranches(this WebApplication webApplication)
    {
        webApplication.MapPost("/branch", async (ISender mediator, CreateBranchCommand request) =>
        {
            var result = await mediator.Send(request);

            return Results.Created($"{result.Id}", result);
        });

        webApplication.MapPost("/branch/add-notification", async (ISender mediator, AddNotificationToBranchCommand request) =>
        {
            var result = await mediator.Send(request);

            return Results.Created($"{result.Id}", result);
        });

        return webApplication;
    }
}
