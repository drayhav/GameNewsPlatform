using GameService.Domain;
using MediatR;

namespace GameService.Application.Queries
{
    public record GetGamesQuery : IRequest<IEnumerable<Game>>;

    public class GetGamesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetGamesQuery, IEnumerable<Game>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GameRepository.GetAllAsync();
        }
    }
}
