using GameService.Domain;
using GameService.Domain.Repositories;

namespace GameService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IGameRepository _gameRepository;
        private readonly GameContext _gameContext;

        public UnitOfWork(IGameRepository gameRepository, GameContext gameContext)
        {
            _gameRepository = gameRepository;
            _gameContext = gameContext;
        }

        public IGameRepository GameRepository => _gameRepository;

        public async Task<int> SaveChangesAsync()
        {
            return await _gameContext.SaveChangesAsync();
        }
    }
}
