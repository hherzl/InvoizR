using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.Application.Common.Contracts;
using InvoizR.Clients;
using InvoizR.Clients.Contracts;
using InvoizR.Clients.ThirdParty;
using InvoizR.Clients.ThirdParty.Contracts;
using InvoizR.Infrastructure.Clients;
using InvoizR.Infrastructure.Clients.ThirdParty;
using InvoizR.Infrastructure.FileExport;
using InvoizR.Infrastructure.Persistence;
using InvoizR.Infrastructure.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoizR.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InvoizRDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("InvoizR")));
        services.AddScoped<IInvoizRDbContext, InvoizRDbContext>();

        services.Configure<SeguridadClientSettings>(options => options.Endpoint = configuration["Clients:Seguridad:Endpoint"]);
        services.AddTransient<ISeguridadClient, SeguridadClient>();

        services.Configure<FirmadorClientSettings>(options => options.Endpoint = configuration["Clients:Firmador:Endpoint"]);
        services.AddScoped<IFirmadorClient, FirmadorClient>();

        services.Configure<FesvClientSettings>(options => options.Endpoint = configuration["Clients:Fesv:Endpoint"]);
        services.AddScoped<IFeSvClient, FeSvClient>();

        services.Configure<SmtpClientSettings>(options =>
        {
            options.Host = configuration["Clients:Smtp:Host"];
            options.Port = Convert.ToInt32(configuration["Clients:Smtp:Port"]);
            options.EnableSsl = Convert.ToBoolean(configuration["Clients:Smtp:EnableSsl"]);
            options.UseDefaultCredentials = Convert.ToBoolean(configuration["Clients:Smtp:UseDefaultCredentials"]);
            options.UserName = configuration["Clients:Smtp:UserName"];
            options.Password = configuration["Clients:Smtp:Password"];
        });
        services.AddScoped<ISmtpClient, SatanCitySmtpClient>();

        services.AddScoped<IQrCodeGenerator, QrCodeGeneratorService>();

        services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

        services.AddScoped<IInvoiceExportStrategy, JsonInvoiceExportStrategy>();
        services.AddScoped<IInvoiceExportStrategy, PdfInvoiceExportStrategy>();

        services.AddScoped<JsonInvoiceExportStrategy>();
        services.AddScoped<PdfInvoiceExportStrategy>();

        services.AddScoped<IFallbackExportStrategy, JsonFallbackExportStrategy>();
        services.AddScoped<IFallbackExportStrategy, PdfFallbackExportStrategy>();

        services.AddScoped<JsonFallbackExportStrategy>();
        services.AddScoped<PdfFallbackExportStrategy>();

        return services;
    }
}
