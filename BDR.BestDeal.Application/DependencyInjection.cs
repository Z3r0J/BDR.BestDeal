using BDR.BestDeal.Application.Helpers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Constants.ApplicationAssembly);

        return services;
    }

    public static IServiceCollection AddClient(this IServiceCollection services)
    {
        services.AddHttpClient(Constants.CargonizerClient, client =>
        {
            client.BaseAddress = new Uri("##");
        });
        
        services.AddHttpClient(Constants.DimAddressClient, client =>
        {
            client.BaseAddress = new Uri("##");
        });

        services.AddHttpClient(Constants.XmlLogisticsClient, client =>
        {
            client.BaseAddress = new Uri("##");
        });

        return services;
    }
}