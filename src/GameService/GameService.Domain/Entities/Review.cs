using GameService.Domain.ValueObjects;

namespace GameService.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; private set; }

        public Guid GameId { get; private set; }

        public Guid UserId { get; private set; }

        public ReviewContent Content { get; private set; }

        public double Rating { get; private set; }

        public Review(Guid id, Guid gameId, Guid userId, ReviewContent content, double rating)
        {
            Id = id;
            GameId = gameId;
            UserId = userId;
            Content = content;
            Rating = rating;
        }
    }
}