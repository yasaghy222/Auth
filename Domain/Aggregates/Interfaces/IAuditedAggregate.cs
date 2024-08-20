namespace Auth.Domain.Aggregates.Interfaces
{
    public interface IAuditedAggregate
    {
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyAt { get; set; }
    }
}