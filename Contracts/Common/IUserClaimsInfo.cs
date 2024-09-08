using Auth.Features.Users.Contracts.Responses;

namespace Auth.Contracts.Common
{
    public interface IUserClaimsInfo
    {
        public string? SessionId { get; set; }
        public UserInfo? UserInfo { get; set; }

        public Ulid LoginOrganizationId { get; set; }
        public string? LoginOrganizationTitle { get; set; }

        public IEnumerable<UserOrganizationInfo>? UserOrganizations { get; set; }
        public IEnumerable<Ulid>? Permissions { get; set; }
    }
}