using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Domain.Aggregates
{
    public class StatusAggregate<TIdentity, TStatus> : BaseAggregate<TIdentity>, IStatusAggregate<TIdentity, TStatus>
    {
        public required TStatus Status { get; set; }
        public string? StatusDescription { get; set; }
    }
}