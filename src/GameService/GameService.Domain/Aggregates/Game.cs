using GameService.Domain.Entities;
using GameService.Domain.Events;
using GameService.Domain.SeedWork;
using GameService.Domain.ValueObjects;

namespace GameService.Domain.Aggregates
{
    public class Game : AggregateRoot
    {
        private readonly List<Genre> _genres = [];
        private readonly List<Review> _reviews = [];

        public Name Name { get; private set; }

        public DateOnly ReleaseDate { get; private set; }

        public IReadOnlyCollection<Genre> Genres => _genres.AsReadOnly();

        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        public double? CurrentScore => _reviews.Count != 0 ? _reviews.Average(r => r.Rating) : null;

        private Game(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public Game(Guid id, Name name, DateOnly releaseDate, IEnumerable<Genre> genres)
        {
            Id = id;
            Name = name;
            ReleaseDate = releaseDate;
            _genres.AddRange(genres);
        }

        public static Game RebuildFromEvents(IEnumerable<IDomainEvent> events)
        {
            return new Game(events.OrderBy(e => e.OccurredOn));
        }

        public void AddReview(Review review)
        {
            var reviewAddedEvent = new ReviewAdded(
                AggregateId: Id,
                OccurredOn: DateTimeOffset.UtcNow,
                ReviewId: Guid.CreateVersion7(),
                UserId: review.UserId,
                Content: review.Content,
                Rating: review.Rating);

            Apply(reviewAddedEvent);
            AddDomainEvent(reviewAddedEvent);
        }

        public void RemoveReview(Review review)
        {
            var reviewRemovedEvent = new ReviewRemoved(
                AggregateId : Id,
                OccurredOn: DateTime.UtcNow,
                ReviewId: review.Id, 
                Rating: review.Rating);

            Apply(reviewRemovedEvent);
            AddDomainEvent(reviewRemovedEvent);
        }

        private void Apply(IDomainEvent @event)
        {
            switch (@event)
            {
                case GameCreated gameCreatedEvent:
                    HandleGameCreatedEvent(gameCreatedEvent);
                    break;
                case ReviewAdded reviewAddedEvent:
                    HandleReviewAddedEvent(reviewAddedEvent);
                    break;
                case ReviewRemoved reviewRemovedEvent:
                    HandleReviewRemovedEvent(reviewRemovedEvent);
                    break;
                default:
                    throw new Exception($"Event {@event.GetType()} was not recognized");
            }
        }

        private void HandleGameCreatedEvent(GameCreated @event)
        {
            Id = @event.AggregateId;
            Name = new Name(@event.Name);
            ReleaseDate = @event.ReleaseDate;
            _genres.AddRange(@event.Genres.Select(g => (Genre)g));
        }

        private void HandleReviewAddedEvent(ReviewAdded @event)
        {
            var review = new Review(@event.ReviewId, @event.AggregateId, @event.UserId, @event.Content, @event.Rating);
            _reviews.Add(review);
        }

        private void HandleReviewRemovedEvent(ReviewRemoved @event)
        {
            _reviews.RemoveAll(r => r.Id == @event.ReviewId);
        }
    }
}