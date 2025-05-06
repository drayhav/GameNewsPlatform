using GameService.Domain.SeedWork;

namespace GameService.Domain.Events;

public record ReviewRemoved(
    Guid AggregateId,
    DateTimeOffset OccurredOn, 
    Guid ReviewId,
    double Rating) : IDomainEvent;