using MH.Mock.Seguridad;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(@"C:\Logs\MH.Mock.Seguridad.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
    .CreateLogger()
    ;

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddSerilog();

    builder.Services.AddAntiforgery();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("Manager", policy =>
        {
            policy.WithOrigins("https://localhost:13890").AllowAnyHeader().AllowAnyMethod();
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseCors("Manager");

    app.UseAntiforgery();

    app.MapAuth();

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
