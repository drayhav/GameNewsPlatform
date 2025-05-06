using GameService.Domain.SeedWork;

namespace GameService.Domain.Events
{
    public class ReviewAddedEvent(Guid ReviewId, Guid AggregateId, Guid UserId, DateTime OccurredOn, string Content, double Rating) 
        : IDomainEvent
    {
        public Guid AggregateId { get; } = AggregateId;

        public DateTimeOffset OccurredOn { get; } = OccurredOn;

        public Guid ReviewId { get; } = ReviewId;

        public Guid UserId { get; } = UserId;

        public double Rating { get; } = Rating;

        public string Content { get; } = Content; 
    }
}