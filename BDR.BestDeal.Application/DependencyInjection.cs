using BDR.BestDeal.Application.Client.Services;
using BDR.BestDeal.Application.Helpers;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Application;

/// <summary>
/// Extension methods for setting up services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds validators from the assembly to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Constants.ApplicationAssembly);
        return services;
    }

    /// <summary>
    /// Configures and adds client services and HTTP clients to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The configuration instance used for setting up HTTP clients.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddClient(this IServiceCollection services, IConfiguration configuration)
    {
        // Adding scoped services for logistics functionalities
        services.AddScoped<XmlLogisticsServices>();
        services.AddScoped<DimAddressServices>();
        services.AddScoped<CargonizerServices>();
        services.AddScoped<MainServices>();

        // Configuring HTTP clients for each service with base addresses from configuration
        services.AddHttpClient(Constants.CargonizerClient, client =>
        {
            client.BaseAddress = new Uri(configuration[Constants.CargonizerClient] ?? "/");
        });

        services.AddHttpClient(Constants.DimAddressClient, client =>
        {
            client.BaseAddress = new Uri(configuration[Constants.DimAddressClient] ?? "/");
        });

        services.AddHttpClient(Constants.XmlLogisticsClient, client =>
        {
            client.BaseAddress = new Uri(configuration[Constants.XmlLogisticsClient] ?? "/");
        });

        return services;
    }
}
