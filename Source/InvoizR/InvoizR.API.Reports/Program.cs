using DinkToPdf.Contracts;
using DinkToPdf;
using InvoizR.API.Reports.Services;
using InvoizR.Application;
using InvoizR.Infrastructure;
using Serilog;
using InvoizR.Application.Common;

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

    builder.Services.AddApplicationServices(builder.Configuration);
    builder.Services.AddInfrastructureServices(builder.Configuration);

    builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

    builder.Services.AddHostedService<Dte01NotificationHostedService>();

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
