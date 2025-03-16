namespace GameService.Domain
{
    public interface IUnitOfWork
    {
        public IGameRepository GameRepository { get; }

        Task<int> SaveChangesAsync();
    }
}
