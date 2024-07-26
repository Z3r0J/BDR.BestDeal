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
}