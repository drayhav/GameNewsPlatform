namespace Common.Messages.IntegrationEvents
{
    public record ReviewAddedIntegrationEvent(Guid GameId, Guid UserId, Guid ReviewId, double Rating, string Content);
}
