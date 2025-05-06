using Common.Messages.IntegrationEvents;
using DataWarehouse.Infrastructure.Entities;
using Microsoft.Extensions.Logging;

namespace DataWarehouse.Infrastructure.Handlers;

public class GameCreatedIntegrationEventHandler
{
    private readonly ILogger<GameCreatedIntegrationEventHandler> logger;
    private readonly DataWarehouseDbContext _dbContext;

    public GameCreatedIntegrationEventHandler(ILogger<GameCreatedIntegrationEventHandler> logger, DataWarehouseDbContext dbContext)
    {
        this.logger = logger;
        _dbContext = dbContext;
    }

    public async Task Handle(GameCreatedIntegrationEvent @event)
    {
        logger.LogInformation($"A game with id {@event.GameId} was created");

        var entity = new GameEntity
        {
            Id = @event.GameId,
            Name = @event.Name,
            ReleaseDate = @event.ReleaseDate,
            Genres = @event.Genres,
            Reviews = [],
            AverageRating = 0
        };

        await _dbContext.Games.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
}