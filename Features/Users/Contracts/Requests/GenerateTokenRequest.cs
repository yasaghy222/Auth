using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Contracts.Requests
{
    public record GenerateTokenRequest
    {
        public required Ulid SessionId { get; set; }
        public required UserInfo UserInfo { get; set; }

        public Ulid LoginOrganizationId { get; set; }
        public required string LoginOrganizationTitle { get; set; }

        public IEnumerable<UserOrganizationInfo>? UserOrganizations { get; set; }
        public IEnumerable<Ulid>? Permissions { get; set; }
    }
}