using System.Reflection;
using InvoizR.Application.Common;
using InvoizR.Application.Reports.Templates;
using InvoizR.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoizR.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(builder => builder.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.Configure<ProcessingSettings>(options =>
        {
            options.OutputPath = configuration["ProcessingSettings:OutputPath"];
            options.LogsPath = configuration["ProcessingSettings:LogsPath"];
            options.DtePath = configuration["ProcessingSettings:DtePath"];
        });

        services.AddScoped<DteSyncHandler>();
        services.AddScoped<DteExporter>();

        services.AddScoped<Dte01ProcessingStatusChanger>();
        services.AddScoped<Dte01TemplateFactory>();

        services.AddScoped<Dte03TemplateFactory>();
        services.AddScoped<Dte03ProcessingStatusChanger>();

        services.AddScoped<Dte04ProcessingStatusChanger>();
        services.AddScoped<Dte04TemplateFactory>();

        services.AddScoped<Dte14ProcessingStatusChanger>();
        services.AddScoped<Dte14TemplateFactory>();

        return services;
    }
}
