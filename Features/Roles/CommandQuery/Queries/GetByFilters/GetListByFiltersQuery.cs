using ErrorOr;
using Auth.Contracts.Enums;
using Auth.Features.Roles.Contracts.Enums;
using Auth.Features.Roles.Contracts.Requests;
using Auth.Features.Roles.Contracts.Responses;

namespace Auth.Features.Roles.CommandQuery.Queries.GetByFilters
{
    public record GetListByFiltersQuery : IRoleQuery<ErrorOr<RolesResponse>>
    {
        public required Ulid OrganizationId { get; set; }

        public required IEnumerable<Ulid>? Ids { get; set; }
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