using GameService.Domain.SeedWork;

namespace GameService.Domain.Repositories;

public interface IEventStore<T> where T : AggregateRoot
{
    Task Store(T aggregate);

    Task<T> GetByIdAsync(Guid id);

    Task RemoveByIdAsync(Guid id);
}