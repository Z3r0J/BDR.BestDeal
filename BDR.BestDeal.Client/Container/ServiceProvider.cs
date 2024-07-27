using BDR.BestDeal.Application;
using BDR.BestDeal.Application.Interfaces;
using BDR.BestDeal.Client.EntryPoint;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Client.Container;

public static class ServiceProvider
{
    private static readonly Lazy<IServiceProvider> LazyServiceProvider = new(BuildServiceProvider);

    public static IServiceProvider Provider => LazyServiceProvider.Value;

    private static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddClient();
        services.AddScoped<IAppStarter, AppStarter>();

        return services.BuildServiceProvider();
    }
}