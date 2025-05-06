using Common.Stuff.Mediator;
using GameService.Application.Exceptions;
using GameService.Domain;
using GameService.Domain.Aggregates;
using GameService.Domain.Repositories;

namespace GameService.Application.Queries
{
    public record GetGameByIdQuery(Guid GameId);

    public class GetGameByIdQueryHandler(IEventStore<Game> eventStore) : IRequestHandler<GetGameByIdQuery, Game>
    {
        private readonly IEventStore<Game> _eventStore;

        public async Task<Game> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await _eventStore.GetByIdAsync(request.GameId);
            return game ?? throw new NotFoundException(typeof(Game), request.GameId);
        }
    }
}
