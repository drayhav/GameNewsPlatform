namespace GameService.Domain.SeedWork
{
    public interface IDomainEvent
    {
        public Guid AggregateId { get; }

        public DateTimeOffset OccurredOn { get; }
    }
}