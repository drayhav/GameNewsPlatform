using GameService.Domain.SeedWork;

namespace GameService.Domain.Events
{
    public class ReviewAddedEvent(Guid Id, Guid AggregateId, Guid UserId, DateTime OccuredOn, string Content, double Rating) 
        : DomainEvent(Id, AggregateId, OccuredOn)
    {
        public double Rating { get; } = Rating;

        public string Content { get; } = Content;

        public Guid UserId { get; } = UserId;

        public override string EventType => nameof(ReviewAddedEvent);
    }
}