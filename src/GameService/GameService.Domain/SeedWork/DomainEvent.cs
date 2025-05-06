namespace GameService.Domain.SeedWork
{
    public abstract class DomainEvent(Guid Id, Guid AggregateId, DateTimeOffset OccurredOn)
    {
        public Guid Id = Id;

        public Guid AggregateId = AggregateId;

        public DateTimeOffset OccurredOn = OccurredOn;

        public abstract string EventType { get; }
    }
}