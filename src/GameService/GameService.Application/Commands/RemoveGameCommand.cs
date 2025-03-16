using GameService.Application.Exceptions;
using GameService.Domain;
using MediatR;

namespace GameService.Application.Commands
{
    public record RemoveGameCommand(Guid Id) : IRequest;

    public class RemoveGameCommandHandler : IRequestHandler<RemoveGameCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveGameCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task Handle(RemoveGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.GameRepository.GetByIdAsync(request.Id);

            if (game is null)
            {
                throw new NotFoundException(typeof(Game), request.Id);
            }

            await _unitOfWork.GameRepository.RemoveByIdAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
