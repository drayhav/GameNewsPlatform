using GameService.Domain.SeedWork;

namespace GameService.Domain.Events;

public record ReviewAdded(
    Guid AggregateId,
    DateTimeOffset OccurredOn,
    Guid ReviewId,
    Guid UserId,
    string Content,
    double Rating) : IDomainEvent;