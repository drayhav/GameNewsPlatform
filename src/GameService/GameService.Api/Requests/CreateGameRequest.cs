using GameService.Application.Commands;

namespace GameService.Api.Requests
{
    public record CreateGameRequest(string Name, List<string> Genres, DateOnly ReleaseDate)
    {
        public CreateGameCommand ToCommand()
        {
            return new CreateGameCommand(Name, Genres, ReleaseDate);
        }
    }
}
