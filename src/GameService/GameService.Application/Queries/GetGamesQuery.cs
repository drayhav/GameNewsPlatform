using Common.Stuff.Mediator;
using GameService.Domain.Aggregates;
using GameService.Domain.Repositories;

namespace GameService.Application.Queries
{
    public record GetGamesQuery;

    public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<Game>>
    {
        private readonly IEventStore<Game> _eventStore;

        public GetGamesQueryHandler(IEventStore<Game> eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<IEnumerable<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            // return await _unitOfWork.GameRepository.GetAllAsync();
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
