using Common.Stuff.Mediator;
using GameService.Application.Exceptions;
using GameService.Domain.Aggregates;
using GameService.Domain.Repositories;

namespace GameService.Application.Commands
{
    public record RemoveGameCommand(Guid Id);

    public class RemoveGameCommandHandler : IRequestHandler<RemoveGameCommand, bool>
    {
        private readonly IEventStore<Game> _eventStore;

        public RemoveGameCommandHandler(IEventStore<Game> eventStore) => _eventStore = eventStore;

        public async Task<bool> Handle(RemoveGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _eventStore.GetByIdAsync(request.Id);

            if (game is null)
            {
                throw new NotFoundException(typeof(Game), request.Id);
            }

            await _eventStore.RemoveByIdAsync(request.Id);

            return true;
        }
    }
}