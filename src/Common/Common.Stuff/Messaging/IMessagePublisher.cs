namespace Common.Stuff.Messaging
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;

        Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
    }
}