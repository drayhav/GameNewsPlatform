using AutoMapper;
using GameService.Domain;
using GameService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameService.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext _gameContext;
        private readonly IMapper _mapper;

        public GameRepository(GameContext gameContext, IMapper mapper)
        {
            _gameContext = gameContext;
            _mapper = mapper;
        }

        public async Task AddAsync(Game game)
        {
            var existingGame = await _gameContext.Games.FirstOrDefaultAsync(x => x.Id == game.Id);

            if (existingGame != null)
            {
                throw new Exception("Game already exists");
            }

            var gameEntity = _mapper.Map<GameEntity>(game);
            await _gameContext.Games.AddAsync(gameEntity);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            var games = await _gameContext.Games.ToListAsync();
            return _mapper.Map<IEnumerable<Game>>(games);
        }

        public async Task<Game> GetByIdAsync(Guid id)
        {
            var game = await _gameContext.Games.FirstOrDefaultAsync(game => game.Id == id);
            var gameEntity = _mapper.Map<Game>(game);
            return gameEntity;
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            var game = await _gameContext.Games.FirstOrDefaultAsync(game => game.Id == id) 
                ?? throw new Exception("Game not found");

            _gameContext.Games.Remove(game);
        }
    }
}