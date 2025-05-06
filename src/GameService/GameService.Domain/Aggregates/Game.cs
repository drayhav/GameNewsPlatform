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
            return new Game(events);
        }

        public void AddReview(Review review)
        {
            var reviewAddedEvent = new ReviewAddedEvent(Guid.CreateVersion7(), Id, review.UserId, 
                DateTime.UtcNow, review.Content, review.Rating);

            Apply(reviewAddedEvent);
            AddDomainEvent(reviewAddedEvent);
        }

        public void RemoveReview(Review review)
        {
            _reviews.Remove(review);
        }

        private void Apply(IDomainEvent @event)
        {
            switch (@event)
            {
                case GameCreatedEvent gameCreatedEvent:
                    HandleGameCreatedEvent(gameCreatedEvent);
                    break;
                case ReviewAddedEvent reviewAddedEvent:
                    HandleReviewAddedEvent(reviewAddedEvent);
                    break;
                default:
                    throw new Exception($"Event {@event.GetType()} was not recognized");
            }
        }

        private void HandleGameCreatedEvent(GameCreatedEvent @event)
        {
            Id = @event.AggregateId;
            Name = new Name(@event.Name);
            ReleaseDate = @event.ReleaseDate;
            _genres.AddRange(@event.Genres.Select(g => (Genre)g));
        }

        private void HandleReviewAddedEvent(ReviewAddedEvent @event)
        {
            var review = new Review(@event.ReviewId, @event.AggregateId, @event.UserId, @event.Content, @event.Rating);
            _reviews.Add(review);
        }
    }
}