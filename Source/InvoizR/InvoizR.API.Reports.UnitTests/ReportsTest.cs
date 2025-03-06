using DinkToPdf;
using DinkToPdf.Contracts;
using InvoizR.Application.Common;
using InvoizR.Application.Common.Contracts;
using InvoizR.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoizR.API.Reports.UnitTests;

public class ReportsTest
{
    private static readonly IConfigurationRoot _configurationRoot;

    static ReportsTest()
    {
        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            ;
    }

    protected readonly IServiceCollection _services;
    protected readonly IServiceProvider _serviceProvider;
    protected ProcessingSettings _processingSettings;
    protected readonly IConverter _converter;
    protected readonly IInvoizRDbContext _dbContext;

    public ReportsTest()
    {
        _services = new ServiceCollection();
        _services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

        _services.AddDbContext<InvoizRDbContext>(options => options.UseSqlServer(_configurationRoot.GetConnectionString("InvoizR")));
        _services.AddScoped<IInvoizRDbContext, InvoizRDbContext>();

        _serviceProvider = _services.BuildServiceProvider();

        _processingSettings = new();
        _configurationRoot.Bind("ProcessingSettings", _processingSettings);

        _converter = _serviceProvider.GetService<IConverter>();

        _dbContext = _serviceProvider.GetService<IInvoizRDbContext>();
    }
}
