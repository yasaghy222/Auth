using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Domain.Aggregates
{
    public class IdentityAggregate<T> : IIdentityAggregate<T>
    {
        public virtual required T Id { get; set; }
    }
}