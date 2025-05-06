using Common.Stuff.Mediator;
using GameService.Domain;
using GameService.Domain.Aggregates;

namespace GameService.Application.Queries
{
    public record GetGamesQuery;

    public class GetGamesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGamesQuery, IEnumerable<Game>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            // return await _unitOfWork.GameRepository.GetAllAsync();
            throw new NotImplementedException("This method is not implemented yet.");
        }
    }
}
