namespace GameService.Domain.Exceptions
{
    public class GameMustHaveAtLeastOneGenreException : Exception
    {
        public GameMustHaveAtLeastOneGenreException() : base("At least one genre must be specified.")
        {
        }
    }
}
