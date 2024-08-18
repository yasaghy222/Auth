namespace Auth.Domain.Aggregates.Interfaces
{
    public interface IBaseAggregate<TId> : IIdentityAggregate<TId>, IAuditedAggregate
    {
    }
}
