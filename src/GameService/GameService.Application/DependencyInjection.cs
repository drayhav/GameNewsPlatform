using Common.Stuff.Mediator;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            RegisterHandlers(services);
            return services;
        }

        private static void RegisterHandlers(IServiceCollection services)
        {
            var applicationAssembly = Assembly.GetAssembly(typeof(DependencyInjection));

            if (applicationAssembly is null)
            {
                return;
            }

            // Find all types that implement IRequestHandler<,>
            var handlerTypes = applicationAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                .ToList();

            // Register each handler type
            foreach (var handlerType in handlerTypes)
            {
                var interfaceType = handlerType.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));

                services.AddScoped(interfaceType, handlerType);
            }
        }
    }
}