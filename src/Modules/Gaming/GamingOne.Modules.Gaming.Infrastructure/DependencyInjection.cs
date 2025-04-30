using GamingOne.Modules.Gaming.Domain.Services;
using GamingOne.Modules.Gaming.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GamingOne.Modules.Gaming.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddGamingInfrastructureModule(this IServiceCollection services)
    {
        services.AddSingleton<Random>();
        services.AddSingleton<IRandomNumberGenerator, DefaultRandomNumberGenerator>();

        return services;
    }
}
