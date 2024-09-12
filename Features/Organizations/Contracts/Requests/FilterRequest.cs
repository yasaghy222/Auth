using Auth.Contracts.Enums;
using Auth.Contracts.Common;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Organizations.Contracts.Enums;

namespace Auth.Features.Organizations.Contracts.Requests
{
    public record OrganizationFilterRequest : IPaginationFilterDto, IDateFilterDto
    {
        public required IEnumerable<Ulid> Ids { get; set; }
        public QueryOrderType IdOrderType { get; set; }
            = QueryOrderType.Descending;

        public string? Title { get; set; }
        public QueryComparisonType TittleComparisonType { get; set; }
             = QueryComparisonType.Contains;
        public QueryOrderType? TitleOrderType { get; set; }

        public OrganizationStatus? Status { get; set; }
        public QueryOrderType? StatusOrderType { get; set; }

        public DateTime? FromCreateAt { get; set; }
        public QueryComparisonType FromCreateAtComparisonType { get; set; }
           = QueryComparisonType.GreaterThanOrEqualTo;

        public DateTime? ToCreateAt { get; set; }
        public QueryComparisonType ToCreateAtComparisonType { get; set; }
                 = QueryComparisonType.LessThanOrEqualTo;

        public QueryOrderType? CreateAtOrderType { get; set; }

        public int? PageSize { get; set; } = 10;
        public int? PageIndex { get; set; } = 1;
    }
}