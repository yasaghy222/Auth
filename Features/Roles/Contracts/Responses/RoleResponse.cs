using Auth.Features.Roles.Contracts.Enums;

namespace Auth.Features.Roles.Contracts.Responses
{
    public record RoleResponse
    {
        public required Ulid Id { get; set; }

        public required string Title { get; set; }

        public required Ulid OrganizationId { get; set; }
        public required string OrganizationTitle { get; set; }

        public RoleStatus Status { get; set; }
    }

    public record RolesResponse
    {
        public IEnumerable<RoleResponse> Items { get; set; } = [];
    };
}