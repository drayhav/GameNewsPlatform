using GameService.Domain.Events;
using GameService.Domain.Exceptions;

namespace GameService.Domain
{
    public class Game : AggregateRoot
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateOnly ReleaseDate { get; private set; }

        public IList<Genre> Genres { get; private set; }

        public Game(Guid id, string name, DateOnly releaseDate, IList<Genre> genres)
        {
            Id = id;
            Name = name;
            ReleaseDate = releaseDate;
            Genres = genres;
        }

        public static Game Create(string name, DateOnly releaseDate, IList<Genre> genres)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new GameNameCannotBeEmptyException();
            }

            if (genres.Count == 0)
            {
                throw new GameMustHaveAtLeastOneGenreException();
            }

            var id = Guid.CreateVersion7();
            var createdEvent = new GameCreatedEvent(id, DateTimeOffset.UtcNow);
            var game = new Game(id, name, releaseDate, genres);
            game.AddDomainEvent(createdEvent);

            return game;
        }
    }
}
