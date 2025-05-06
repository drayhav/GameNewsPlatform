using GameService.Domain.Entities;

namespace GameService.Infrastructure.Entities;

public class ReviewEntity
{
    public Guid Id { get; set; }

    public Guid GameId { get; set; }

    public Guid UserId { get; set; }

    public string Content { get; set; }

    public double Rating { get; set; }

    public static ReviewEntity FromReview(Review review)
    {
        return new ReviewEntity
        {
            Id = review.Id,
            GameId = review.GameId,
            UserId = review.UserId,
            Content = review.Content.Value,
            Rating = review.Rating,
        };
    }
}