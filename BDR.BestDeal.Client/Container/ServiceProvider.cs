using BDR.BestDeal.Application;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Client.EntryPoint;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Client.Container;

/// <summary>
/// Provides a centralized service provider for managing service instances and dependencies.
/// </summary>
public static class ServiceProvider
{
    /// <summary>
    /// Lazily initializes a singleton instance of the service provider.
    /// </summary>
    private static readonly Lazy<IServiceProvider> LazyServiceProvider = new(BuildServiceProvider);

    /// <summary>
    /// Gets the single global instance of the service provider.
    /// </summary>
    public static IServiceProvider Provider => LazyServiceProvider.Value;

    /// <summary>
    /// Builds and configures the service provider from service collections and configurations.
    /// </summary>
    /// <returns>A fully configured <see cref="IServiceProvider"/>.</returns>
    private static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        // Setting up configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Adding configuration as a singleton to ensure it's available application-wide.
        services.AddSingleton<IConfiguration>(configuration);

        // Adding client services configured in the Application project.
        services.AddClient(configuration);

        // Registering the AppStarter class which implements IAppStarter interface.
        services.AddScoped<IAppStarter, AppStarter>();

        return services.BuildServiceProvider();
    }
}