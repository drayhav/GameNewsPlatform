using Common.Stuff.Mediator;
using GameService.Domain.Aggregates;
using GameService.Domain.Entities;
using GameService.Domain.Repositories;
using GameService.Domain.ValueObjects;

namespace GameService.Application.Commands
{
    public record CreateReviewCommand(Guid GameId, Guid UserId, string Content, double Rating);

    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IEventStore<Game> _eventStore;

        public CreateReviewCommandHandler(IEventStore<Game> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var content = ReviewContent.Create(request.Content);
            var game = await _eventStore.GetByIdAsync(request.GameId);

            var review = new Review(
                id: Guid.CreateVersion7(),
                gameId: request.GameId,
                userId: request.UserId,
                content: content,
                rating: request.Rating);

            game.AddReview(review);

            await _eventStore.Store(game);
            return review.Id;
        }
    }
}