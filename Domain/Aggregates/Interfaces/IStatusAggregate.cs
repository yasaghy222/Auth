namespace Auth.Domain.Aggregates.Interfaces
{
    public interface IStatusAggregate<TIdentity, TStatus> : IBaseAggregate<TIdentity>
    {
        public TStatus Status { get; set; }
        public string? StatusDescription { get; set; }
    }
}