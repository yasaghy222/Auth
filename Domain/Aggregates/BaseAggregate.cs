using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Domain.Aggregates
{
    public class BaseAggregate<TId> : AuditedAggregate, IBaseAggregate<TId>
    {
        public virtual required TId Id { get; set; }
    }
}