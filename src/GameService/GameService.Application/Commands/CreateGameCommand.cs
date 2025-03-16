using GameService.Domain;
using MediatR;

namespace GameService.Application.Commands
{
    public record CreateGameCommand(string Name, IEnumerable<Genre> Genres, DateOnly ReleaseDate) : IRequest<Guid>;

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = Game.Create(request.Name, request.ReleaseDate, request.Genres.ToList());

            await _unitOfWork.GameRepository.AddAsync(game);
            await _unitOfWork.SaveChangesAsync();
            return game.Id;
        }
    }
}
