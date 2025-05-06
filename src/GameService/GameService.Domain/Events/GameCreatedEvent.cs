using GameService.Domain.SeedWork;

namespace GameService.Domain.Events
{
    public class GameCreatedEvent(Guid AggregateId, DateTimeOffset OccurredOn, string Name, DateOnly ReleaseDate, IList<string> Genres)
        : IDomainEvent
    {
        public Guid AggregateId { get; } = AggregateId;

        public DateTimeOffset OccurredOn { get; } = OccurredOn;

        public string Name { get; } = Name;

        public DateOnly ReleaseDate { get; } = ReleaseDate;

        public IList<string> Genres { get; } = Genres;
    }
}