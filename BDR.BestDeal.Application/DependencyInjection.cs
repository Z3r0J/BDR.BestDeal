using BDR.BestDeal.Application.Client.Services;
using BDR.BestDeal.Application.Helpers;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BDR.BestDeal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Constants.ApplicationAssembly);

        return services;
    }

    public static IServiceCollection AddClient(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddScoped<XmlLogisticsServices>();
        services.AddScoped<DimAddressServices>();
        services.AddScoped<CargonizerServices>();
        services.AddScoped<MainServices>();

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