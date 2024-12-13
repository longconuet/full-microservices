using Contracts.Common.Events;
using Contracts.Domains.Interfaces;

namespace Contracts.Common.Interfaces
{
    public interface IEventEntity
    {
        IReadOnlyCollection<BaseEvent> DomainEvents();
        void AddDomainEvent(BaseEvent domainEvent);
        void RemoveDomainEvent(BaseEvent domainEvent);
        void ClearDomainEvent();
    }

    public interface IEventEntity<T> : IEntityBase<T>, IEventEntity { }
}
