namespace GameService.Domain
{
    public interface IGameRepository
    {
        Task AddAsync(Game game);

        Task<Game> GetByIdAsync(Guid id);

        Task<IEnumerable<Game>> GetAllAsync();

        Task RemoveByIdAsync(Guid id);
    }
}
