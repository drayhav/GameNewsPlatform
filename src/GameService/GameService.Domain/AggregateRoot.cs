namespace GameService.Domain
{
    public abstract class AggregateRoot
    {
        private List<DomainEvent> _domainEvents = [];

        public Guid Id { get; protected set; }

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
