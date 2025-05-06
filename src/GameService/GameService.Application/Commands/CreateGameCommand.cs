using Common.Stuff.Mediator;
using GameService.Domain;
using GameService.Domain.Factories;

namespace GameService.Application.Commands
{
    public record CreateGameCommand(string Name, List<string> Genres, DateOnly ReleaseDate);

    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateGameCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = GameFactory.Create(request.Name, request.ReleaseDate, request.Genres);

            await _unitOfWork.GameRepository.AddAsync(game);
            await _unitOfWork.SaveChangesAsync();
            return game.Id;
        }
    }
}
