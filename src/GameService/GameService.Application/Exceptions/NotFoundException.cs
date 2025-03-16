namespace GameService.Application.Exceptions
{
    public class NotFoundException(Type type, Guid id) : Exception($"Entity of type {type.Name} with id {id} was not found.")
    {
        public Type Type { get; private set; } = type;

        public Guid Id { get; private set; } = id;
    }
}