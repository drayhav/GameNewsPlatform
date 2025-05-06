namespace GameService.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        private List<IDomainEvent> _uncomittedEvents = [];

        public Guid Id { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> UncomittedEvents => _uncomittedEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _uncomittedEvents.Add(domainEvent);
        }

        public void ClearUncomittedEvents()
        {
            _uncomittedEvents.Clear();
        }
    }
}
