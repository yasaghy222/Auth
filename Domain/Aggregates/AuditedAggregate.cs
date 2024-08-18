using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Domain.Aggregates
{
    public class AuditedAggregate : IAuditedAggregate
    {
        public virtual DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public virtual DateTime ModifyAt { get; set; } = DateTime.UtcNow;
    }
}