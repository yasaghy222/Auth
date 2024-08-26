using Auth.Contracts.Request;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Contracts.Requests
{
    public record UserFilterRequest : IUserQuery<UserResponses>
    {
        public required IEnumerable<Ulid> Organizations { get; set; }

        public string? Name { get; set; }
        public QueryComparisonType NameComparisonType { get; set; }
             = QueryComparisonType.Contains;

        public string? Family { get; set; }
        public QueryComparisonType FamilyComparisonType { get; set; }
            = QueryComparisonType.Contains;


        public IEnumerable<string>? Usernames { get; set; }
        public QueryComparisonType UsernamesComparisonType { get; set; }
            = QueryComparisonType.Contains;

        public IEnumerable<string>? Phones { get; set; }
        public QueryComparisonType PhonesComparisonType { get; set; }
            = QueryComparisonType.Contains;

        public bool? IsPhoneValid { get; set; }

        public IEnumerable<string>? Emails { get; set; }
        public QueryComparisonType EmailsComparisonType { get; set; }
            = QueryComparisonType.Contains;

        public bool? IsEmailValid { get; set; }

        public UserStatus? Status { get; set; }
    }
}