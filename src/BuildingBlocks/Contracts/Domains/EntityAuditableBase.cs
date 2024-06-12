using Contracts.Domains.Interfaces;

namespace Contracts.Domains
{
    public abstract class EntityAuditableBase<T> : EntityBase<T>, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
