namespace GameService.Domain.Events
{
    class GameCreatedEvent(Guid id, DateTimeOffset occuredOn) : DomainEvent(id, occuredOn)
    {
        public override string EventType => nameof(GameCreatedEvent);
    }
}
