using Common.Messages.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace DataWarehouse.Infrastructure.Handlers;

public class GameCreatedIntegrationEventHandler
{
    private readonly ILogger<GameCreatedIntegrationEventHandler> logger;

    public GameCreatedIntegrationEventHandler(ILogger<GameCreatedIntegrationEventHandler> logger)
    {
        this.logger = logger;
    }

    public async Task Handle(GameCreatedIntegrationEvent @event)
    {
        logger.LogInformation($"A game with id {@event.GameId} was created");
        await Task.CompletedTask;
    }
}