using InvoizR.Clients;
using InvoizR.Clients.Contracts;

namespace InvoizR.GUI.InvoiceManager.Client;

public static class ConfigureServices
{
    public static IServiceCollection AddClients(this IServiceCollection services)
    {
        services.AddScoped<IInvoizRClient, InvoizRClient>();
        services.Configure<InvoizRClientSettings>(options =>
        {
            options.Endpoint = "https://localhost:13880";
        });

        return services;
    }
}
