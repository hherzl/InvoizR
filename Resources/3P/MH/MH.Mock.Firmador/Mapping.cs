namespace MH.Mock.Firmador;

public static partial class Mapping
{
    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        services.AddTransient<ILogger>(p =>
        {
            var loggerFactory = p.GetRequiredService<ILoggerFactory>();
            return loggerFactory.CreateLogger("MhLogger");
        });

        return services;
    }
}
