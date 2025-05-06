namespace Common.Messages.IntegrationEvents
{
    public record GameCreatedIntegrationEvent(Guid GameId, string Name, DateOnly ReleaseDate, List<string> Genres);
}
