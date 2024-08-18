using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Domain.Aggregates
{
    public class BaseAggregate<TId> : IBaseAggregate<TId>
    {
        public virtual required TId Id { get; set; }
        public virtual DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public virtual DateTime ModifyAt { get; set; } = DateTime.UtcNow;
    }
}