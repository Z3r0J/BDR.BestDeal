using BDR.BestDeal.Application;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Client.EntryPoint;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Client.Container;

public static class ServiceProvider
{
    private static readonly Lazy<IServiceProvider> LazyServiceProvider = new(BuildServiceProvider);

    public static IServiceProvider Provider => LazyServiceProvider.Value;

    private static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddClient(configuration);
        services.AddScoped<IAppStarter, AppStarter>();

        return services.BuildServiceProvider();
    }
}