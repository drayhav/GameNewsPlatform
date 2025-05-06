using GameService.Domain.Events;
using Marten.Events.Aggregation;

namespace GameService.Infrastructure.Projections;

public record GameRatingInfo(Guid Id, double AverageRating, int TotalReviews);

/// <summary>
/// An example of a projection to be used internally in the GameService application.
/// </summary>
public class GameRatingInfoProjection : SingleStreamProjection<GameRatingInfo>
{
    public GameRatingInfo Create(GameCreated gameCreatedEvent)
    {
        return new GameRatingInfo(
            Id: gameCreatedEvent.AggregateId,
            AverageRating: 0,
            TotalReviews: 0);
    }

    public GameRatingInfo Apply(ReviewAdded reviewAddedEvent, GameRatingInfo current) =>
        current with
        {
            TotalReviews = current.TotalReviews + 1,
            AverageRating = (current.AverageRating * current.TotalReviews + reviewAddedEvent.Rating) / (current.TotalReviews + 1)
        };

    public GameRatingInfo Apply(ReviewRemoved reviewRemovedEvent, GameRatingInfo current) =>
        current with
        {
            TotalReviews = current.TotalReviews - 1,
            AverageRating = (current.AverageRating * current.TotalReviews - reviewRemovedEvent.Rating) / (current.TotalReviews - 1)
        };
}