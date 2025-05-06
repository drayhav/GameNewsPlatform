using GameService.Domain.SeedWork;

namespace GameService.Domain.Events;

public record ReviewAddedEvent(
    Guid AggregateId,
    DateTimeOffset OccurredOn,
    Guid ReviewId,
    Guid UserId,
    string Content,
    double Rating) : IDomainEvent;