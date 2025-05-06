namespace DataWarehouse.Infrastructure.Entities;

public class GameEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public List<string> Genres { get; set; }

    public List<ReviewEntity> Reviews { get; set; }

    public double AverageRating { get; set; }
}
