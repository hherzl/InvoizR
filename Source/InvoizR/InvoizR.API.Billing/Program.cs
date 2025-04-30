using InvoizR.API.Billing;
using InvoizR.API.Billing.Services;
using InvoizR.Application;
using InvoizR.Application.Common;
using InvoizR.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Serilog;

var apiSettings = new ApiSettings();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddProblemDetails();

    builder.Configuration.Bind("ApiSettings", apiSettings);

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File(apiSettings.LogPath, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
        .CreateLogger()
        ;

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddSerilog();

    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddScoped<InvoizRInitializer>();
    builder.Services.AddHostedService<Dte01HostedService>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Manager", policy =>
        {
            policy.WithOrigins("https://localhost:13890").AllowAnyHeader().AllowAnyMethod();
        });
    });

    builder.Services.AddSignalR();
    builder.Services.AddResponseCompression(opts =>
    {
        opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]);
    });

    var app = builder.Build();

    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async httpContext =>
        {
            var pds = httpContext.RequestServices.GetService<IProblemDetailsService>();
            if (pds == null || !await pds.TryWriteAsync(new() { HttpContext = httpContext }))
            {
                // Fallback behavior
                await httpContext.Response.WriteAsync("Fallback: An error occurred.");
            }
        });
    });

    app.UseResponseCompression();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    if (args.Contains("--seed"))
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<InvoizRInitializer>();
        await initializer.SeedAsync();
        return;
    }

    app.UseHttpsRedirection();

    app.UseCors("Manager");

    app.MapCompanies();
    app.MapBranches();
    app.MapPos();
    app.MapInvoiceTypes();
    app.MapInvoices();
    app.MapDte01();

    app.MapHub<BillingHub>("/billinghub");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
