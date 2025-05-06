using Common.Messages.IntegrationEvents;
using Common.Stuff.Messaging;
using GameService.Domain.Aggregates;
using GameService.Domain.Events;
using GameService.Domain.Repositories;
using GameService.Domain.SeedWork;
using Marten;
using Wolverine.Marten;

namespace GameService.Infrastructure.Repositories;

public class GameRepository : IEventStore<Game>
{
    private readonly IDocumentSession _session;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IMartenOutbox _outbox;

    public GameRepository(IDocumentSession session, IMessagePublisher messagePublisher, IMartenOutbox outbox)
    {
        _session = session;
        _messagePublisher = messagePublisher;
        _outbox = outbox;
    }

    public async Task Store(Game game)
    {
        var stream = await _session.Events.FetchStreamAsync(game.Id);

        if (stream is null)
        {
            var streamId = game.Id;
            var events = game.UncomittedEvents;
            _session.Events.StartStream<Game>(streamId, events);
        }
        else
        {
            _session.Events.Append(game.Id, game.UncomittedEvents);
        }

        // Need to add an outbox pattern here later.

        foreach (var @event in game.UncomittedEvents)
        {
            if (@event is GameCreated gameCreated)
            {
                var integrationEvent = new GameCreatedIntegrationEvent(
                   GameId: game.Id,
                   Name: game.Name.Value,
                   ReleaseDate: game.ReleaseDate,
                   Genres: game.Genres.Select(g => g.Name).ToList());
                //await _messagePublisher.PublishAsync(integrationEvent);
                await _outbox.SendAsync(integrationEvent);
            }
            else if (@event is ReviewAdded reviewAdded)
            {
                var integrationEvent = new ReviewAddedIntegrationEvent(
                    GameId: game.Id,
                    ReviewId: reviewAdded.ReviewId,
                    UserId: reviewAdded.UserId,
                    Rating: reviewAdded.Rating,
                    Content: reviewAdded.Content);

                //await _messagePublisher.PublishAsync(integrationEvent);
                await _outbox.SendAsync(integrationEvent);
            }
        }

        await _session.SaveChangesAsync();
        game.ClearUncomittedEvents();
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        var events = await _session.Events.FetchStreamAsync(id);

        if (events == null || events.Count == 0)
        {
            throw new Exception($"Game with ID {id} not found.");
        }

        return Game.RebuildFromEvents(events.Select(e => (IDomainEvent)e.Data));
    }

    public async Task RemoveByIdAsync(Guid id)
    {
        _session.Events.ArchiveStream(id);
        await _session.SaveChangesAsync();
    }
}