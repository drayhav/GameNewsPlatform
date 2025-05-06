using GameService.Domain.Aggregates;
using GameService.Domain.Repositories;
using GameService.Domain.SeedWork;
using Marten;

namespace GameService.Infrastructure.Repositories;

public class GameRepository : IGameRepository
{
    private readonly IDocumentSession _session;

    public GameRepository(IDocumentSession session) => _session = session;

    public async Task AddAsync(Game game)
    {
        var streamId = game.Id;
        var events = game.UncomittedEvents;

        _session.Events.StartStream<Game>(streamId, events);
        await _session.SaveChangesAsync();

        game.ClearUncomittedEvents();
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        try
        {

            var events = await _session.Events.FetchStreamAsync(id);

            if (events == null || events.Count == 0)
            {
                throw new Exception($"Game with ID {id} not found.");
            }

            return Game.RebuildFromEvents(events.Select(e => (DomainEvent)e.Data));
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        _session.Events.ArchiveStream(id);
        await _session.SaveChangesAsync();
    }
}