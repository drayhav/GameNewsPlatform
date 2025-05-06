using GameService.Domain.Aggregates;

namespace GameService.Infrastructure.Entities;

public class GameEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public IEnumerable<string> Genres { get; set; } = [];

    public IEnumerable<ReviewEntity> Reviews { get; set; } = [];

    public static GameEntity FromGame(Game game)
    {
        return new GameEntity
        {
            Id = game.Id,
            Name = game.Name.Value,
            ReleaseDate = game.ReleaseDate,
            Genres = game.Genres.Select(g => g.Name),
            Reviews = game.Reviews.Select(r => new ReviewEntity 
            {
                Id = r.Id,
                GameId = r.GameId,
                UserId = r.UserId,
                Content = r.Content,
                Rating = r.Rating                
            })
        };
    }
}