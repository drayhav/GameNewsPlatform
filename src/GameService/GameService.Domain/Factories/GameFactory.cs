using GameService.Domain.Aggregates;
using GameService.Domain.Events;
using GameService.Domain.Exceptions;
using GameService.Domain.ValueObjects;

namespace GameService.Domain.Factories
{
    public static class GameFactory
    {
        public static Game Create(string name, DateOnly releaseDate, IList<string> genres)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new GameNameCannotBeEmptyException();
            }

            if (genres == null || genres.Count == 0)
            {
                throw new GameMustHaveAtLeastOneGenreException();
            }

            var gameId = Guid.CreateVersion7();
            var createdEvent = new GameCreated(gameId, DateTimeOffset.UtcNow, name, releaseDate, genres);

            var game = Game.RebuildFromEvents(new[] { createdEvent });
            game.AddDomainEvent(createdEvent);

            return game;
        }
    }
}