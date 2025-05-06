using GameService.Domain;
using GameService.Domain.Repositories;
using GameService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
