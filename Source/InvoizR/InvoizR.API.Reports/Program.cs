using InvoizR.API.Reports.Services;
using InvoizR.Application;
using InvoizR.Application.Common;
using InvoizR.Infrastructure;
using Serilog;

var apiSettings = new ApiSettings();

try
{
    var builder = WebApplication.CreateBuilder(args);

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

    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);

    var app = builder.Build();

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
