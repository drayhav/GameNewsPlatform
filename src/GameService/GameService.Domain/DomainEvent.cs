namespace GameService.Domain
{
    public abstract class DomainEvent(Guid Id, DateTimeOffset OccurredOn)
    {
        public Guid Id = Id;

        public DateTimeOffset OccurredOn = OccurredOn;

        public abstract string EventType { get; }
    }
}
