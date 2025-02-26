using InvoizR.Application;
using InvoizR.Infrastructure;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoizR.API.Billing.UnitTests;

public class BillingTest
{
    private static readonly IConfigurationRoot _configurationRoot;

    static BillingTest()
    {
        _configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            ;
    }

    protected readonly IServiceCollection _services;
    protected readonly IServiceProvider _serviceProvider;
    protected readonly IMediator _mediator;

    public BillingTest()
    {
        _services = new ServiceCollection();

        _services.AddApplicationServices(_configurationRoot);
        _services.AddInfrastructureServices(_configurationRoot);

        _serviceProvider = _services.BuildServiceProvider();

        _mediator = _serviceProvider.GetService<IMediator>();
    }
}
