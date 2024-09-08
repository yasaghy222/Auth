using Auth.Contracts.Enums;
using Auth.Contracts.Common;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.EndPoints.GetListByFilters
{
    public record UserGetListByFiltersDto : IPaginationFilterDto, IDateFilterDto
    {
        public IEnumerable<Ulid>? Ids { get; set; }

        public QueryOrderType IdOrderType { get; set; }
            = QueryOrderType.Descending;

        public required IEnumerable<Ulid>? Organizations { get; set; }
        public QueryOrderType? OrganizationOrderType { get; set; }

        public string? FullName { get; set; }
        public QueryComparisonType FullNameComparisonType { get; set; }
             = QueryComparisonType.Contains;
        public QueryOrderType? FullNameOrderType { get; set; }

        public IEnumerable<string>? Usernames { get; set; }
        public QueryComparisonType UsernamesComparisonType { get; set; }
            = QueryComparisonType.Contains;
        public QueryOrderType? UsernameOrderType { get; set; }


        public IEnumerable<string>? Phones { get; set; }
        public QueryComparisonType PhonesComparisonType { get; set; }
            = QueryComparisonType.Contains;
        public QueryOrderType? PhoneOrderType { get; set; }


        public bool? IsPhoneValid { get; set; }
        public QueryOrderType? IsPhoneValidOrderType { get; set; }


        public IEnumerable<string>? Emails { get; set; }
        public QueryComparisonType EmailsComparisonType { get; set; }
            = QueryComparisonType.Contains;
        public QueryOrderType? EmailOrderType { get; set; }


        public bool? IsEmailValid { get; set; }
        public QueryOrderType? IsEmailValidOrderType { get; set; }


        public UserStatus? Status { get; set; }
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