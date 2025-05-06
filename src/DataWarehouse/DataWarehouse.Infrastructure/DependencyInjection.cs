using DataWarehouse.Infrastructure.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DataWarehouse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<GameCreatedIntegrationEventHandler>();
        return services;
    }
}