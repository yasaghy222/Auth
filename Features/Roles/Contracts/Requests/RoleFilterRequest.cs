using Auth.Contracts.Enums;
using Auth.Features.Roles.Contracts.Enums;

namespace Auth.Features.Roles.Contracts.Requests
{
    public record RoleFilterRequest
    {
        public Ulid OrganizationId { get; set; }

        public IEnumerable<Ulid>? Ids { get; set; }
        public QueryOrderType IdOrderType { get; set; }
            = QueryOrderType.Descending;

        public string? Title { get; set; }
        public QueryComparisonType TittleComparisonType { get; set; }
             = QueryComparisonType.Contains;
        public QueryOrderType? TitleOrderType { get; set; }

        public RoleStatus? Status { get; set; }
        public QueryOrderType? StatusOrderType { get; set; }
    }
}