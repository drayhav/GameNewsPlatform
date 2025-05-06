using GameService.Domain.SeedWork;

namespace GameService.Domain.Events;

public record GameCreated(
    Guid AggregateId,
    DateTimeOffset OccurredOn,
    string Name,
    DateOnly ReleaseDate,
    IList<string> Genres) : IDomainEvent;