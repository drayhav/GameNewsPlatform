using GameService.Domain.SeedWork;

namespace GameService.Domain.Events
{
    public class GameCreatedEvent(Guid Id, Guid GameId, DateTimeOffset OccuredOn, string Name, DateOnly ReleaseDate, IList<string> Genres)
        : DomainEvent(Id, GameId, OccuredOn)
    {

        public string Name { get; } = Name;

        public DateOnly ReleaseDate { get; } = ReleaseDate;

        public IList<string> Genres { get; } = Genres;

        public override string EventType => nameof(GameCreatedEvent);
    }
}