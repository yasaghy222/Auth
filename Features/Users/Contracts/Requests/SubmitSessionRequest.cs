using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Users.CommandQuery.Commands.Login;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Contracts.Requests
{
    public record SubmitSessionRequest
    {
        public required Ulid SessionId { get; set; }
        public required Ulid UserId { get; set; }
        public required string IP { get; set; }
        public required Ulid OrganizationId { get; set; }
        public required string OrganizationTitle { get; set; }
        public required string UniqueId { get; set; }
        public required SessionPlatform Platform { get; set; }
        public required DateTime ExpireAt { get; set; }
    }
}