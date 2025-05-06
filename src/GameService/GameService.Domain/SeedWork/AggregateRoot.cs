namespace GameService.Domain.SeedWork
{
    public abstract class AggregateRoot
    {
        private List<DomainEvent> _uncomittedEvents = [];

        public Guid Id { get; protected set; }

        public IReadOnlyCollection<DomainEvent> UncomittedEvents => _uncomittedEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _uncomittedEvents.Add(domainEvent);
        }

        public void ClearUncomittedEvents()
        {
            _uncomittedEvents.Clear();
        }
    }
}
