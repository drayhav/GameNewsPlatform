using GameService.Domain.Aggregates;

namespace GameService.Domain.Repositories;

public interface IGameRepository
{
    Task Store(Game game);

    Task<Game> GetByIdAsync(Guid id);

    Task RemoveByIdAsync(Guid id);
}
