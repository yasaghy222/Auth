namespace Auth.Features.Users.Contracts.Responses
{
    public record UserOrganizationInfo
    {
        public required Ulid OrganizationId { get; set; }
        public required string OrganizationTitle { get; set; }
        public required Ulid RoleId { get; set; }
        public required string RoleTitle { get; set; }
    }
}