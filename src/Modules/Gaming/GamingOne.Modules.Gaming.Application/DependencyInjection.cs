using Microsoft.Extensions.DependencyInjection;

namespace GamingOne.Modules.Gaming.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddGamingApplcationModule(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddSingleton<GameManager>();

        return services;
    }
}
