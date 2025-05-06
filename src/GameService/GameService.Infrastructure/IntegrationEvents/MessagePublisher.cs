using Common.Stuff.Messaging;
using GameService.Application;
using Wolverine;

namespace GameService.Infrastructure.IntegrationEvents;

public class WolverineMessagePublisher : IMessagePublisher
{
    private readonly IMessageBus _messageBus;

    public WolverineMessagePublisher(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await _messageBus.PublishAsync(message);
    }

    public async Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : class
    {
        await _messageBus.SendAsync(message);
    }
}
