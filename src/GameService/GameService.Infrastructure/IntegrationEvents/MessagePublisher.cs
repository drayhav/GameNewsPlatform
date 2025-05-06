using GameService.Application.IntegrationEvents;
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
}
