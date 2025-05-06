using Microsoft.Extensions.DependencyInjection;

namespace Common.Stuff.Mediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : notnull
    {
        var handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();

        return handler is null
            ? throw new InvalidOperationException($"No handler found for request of type {typeof(TRequest).Name}")
            : await handler.Handle(request, cancellationToken);
    }
}