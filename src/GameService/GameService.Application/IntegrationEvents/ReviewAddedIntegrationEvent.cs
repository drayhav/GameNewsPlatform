namespace GameService.Application.IntegrationEvents
{
    public record ReviewAddedIntegrationEvent(Guid GameId, Guid UserId, Guid ReviewId, double Rating, string Content);
}
