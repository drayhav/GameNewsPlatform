namespace GameService.Domain.Exceptions
{
    public class GameNameCannotBeEmptyException : Exception
    {
        public GameNameCannotBeEmptyException() : base("Game name cannot be empty.")
        {
        }
    }
}
