namespace Auth.Domain.Aggregates.Interfaces
{
    public interface IIdentityAggregate<TId>
    {
        public TId Id { get; set; }
    }
}