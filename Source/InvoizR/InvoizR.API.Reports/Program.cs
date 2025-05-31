using InvoizR.API.Reports.Services;
using InvoizR.Application;
using InvoizR.Application.Common;
using InvoizR.Infrastructure;
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

    builder.Services.AddHostedService<Dte01NotificationHostedService>();
    builder.Services.AddHostedService<Dte03NotificationHostedService>();

    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);

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

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

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
