using GameService.Application.Commands;

namespace GameService.Api.Requests
{
    public record AddReviewRequest(Guid GameId, Guid UserId, double Rating, string Content)
    {
        public CreateReviewCommand ToCommand()
        {
            return new CreateReviewCommand(GameId, UserId, Content, Rating);
        }
    }
}