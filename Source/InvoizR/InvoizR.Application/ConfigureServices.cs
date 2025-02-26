using System.Reflection;
using InvoizR.Application.Common;
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

        services.AddScoped<DteHandler>();
        services.AddScoped<InvoiceProcessingService>();

        return services;
    }
}
