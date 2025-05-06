using GameService.Domain.Aggregates;
using GameService.Domain.Repositories;

namespace GameService.Infrastructure.Repositories
{
    public class FakeGameRepository : IEventStore<Game>
    {
        private List<Game> _games;
        public FakeGameRepository()
        {
            _games = new List<Game>();
        }

        public Task Store(Game game)
        {
            _games.Add(game);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Game>> GetAllAsync()
        {
            return Task.FromResult(_games.AsEnumerable());
        }

        public Task<Game> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_games.FirstOrDefault(g => g.Id == id));
        }

        public Task RemoveByIdAsync(Guid id)
        {
            _games.RemoveAll(g => g.Id == id);
            return Task.CompletedTask;
        }
    }
}