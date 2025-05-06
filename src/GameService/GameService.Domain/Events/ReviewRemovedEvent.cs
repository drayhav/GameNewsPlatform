using GameService.Domain.SeedWork;

namespace GameService.Domain.Events;

public record ReviewRemovedEvent(
    Guid AggregateId, 
    DateTimeOffset OccurredOn, 
    Guid ReviewId) : IDomainEvent;