using Common.Stuff.Messaging;
using GameService.Domain.Aggregates;
using GameService.Domain.Repositories;
using GameService.Infrastructure.IntegrationEvents;
using GameService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GameService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEventStore<Game>, GameRepository>();
            services.AddScoped<IMessagePublisher, WolverineMessagePublisher>();

            return services;
        }
    }
}
