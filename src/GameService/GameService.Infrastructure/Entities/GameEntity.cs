namespace GameService.Infrastructure.Entities
{
    public class GameEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public IEnumerable<int> Genres { get; set; }
    }
}
