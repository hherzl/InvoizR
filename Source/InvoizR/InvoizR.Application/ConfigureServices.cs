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
            options.InvoicesPath = configuration["ProcessingSettings:InvoicesPath"];
            options.NotificationsPath = configuration["ProcessingSettings:NotificationsPath"];
            options.FallbacksPath = configuration["ProcessingSettings:FallbacksPath"];
        });

        services.AddScoped<InvoiceSyncHandler>();

        services.AddScoped<Dte01SyncStatusChanger>();
        services.AddScoped<Dte01TemplateFactory>();

        services.AddScoped<Dte03TemplateFactory>();
        services.AddScoped<Dte03SyncStatusChanger>();

        services.AddScoped<Dte04SyncStatusChanger>();
        services.AddScoped<Dte04TemplateFactory>();

        services.AddScoped<Dte05SyncStatusChanger>();
        services.AddScoped<Dte05TemplateFactory>();

        services.AddScoped<Dte06SyncStatusChanger>();
        services.AddScoped<Dte06TemplateFactory>();

        services.AddScoped<Dte14SyncStatusChanger>();
        services.AddScoped<Dte14TemplateFactory>();

        services.AddScoped<FallbackTemplateFactory>();

        services.AddScoped<InvoiceCancellationHandler>();

        services.AddScoped<InvoiceExporter>();
        services.AddScoped<FallbackInvoiceExporter>();

        services.AddScoped<FallbackExporter>();
        services.AddScoped<FallbackSyncHandler>();

        return services;
    }
}
