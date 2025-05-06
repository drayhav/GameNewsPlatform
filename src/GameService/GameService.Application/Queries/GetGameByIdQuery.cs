using Common.Stuff.Mediator;
using GameService.Application.Exceptions;
using GameService.Domain;
using GameService.Domain.Aggregates;

namespace GameService.Application.Queries
{
    public record GetGameByIdQuery(Guid GameId);

    public class GetGameByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGameByIdQuery, Game>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Game> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(request.GameId);
            return game ?? throw new NotFoundException(typeof(Game), request.GameId);
        }
    }
}
