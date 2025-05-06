using Common.Stuff.Mediator;
using GameService.Domain.Aggregates;
using GameService.Domain.Factories;
using GameService.Domain.Repositories;

namespace GameService.Application.Commands
{
    public record CreateGameCommand(string Name, List<string> Genres, DateOnly ReleaseDate);

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IEventStore<Game> _eventStore;

        public CreateGameCommandHandler(IEventStore<Game> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = GameFactory.Create(request.Name, request.ReleaseDate, request.Genres);

            await _eventStore.Store(game);
            return game.Id;
        }
    }
}
