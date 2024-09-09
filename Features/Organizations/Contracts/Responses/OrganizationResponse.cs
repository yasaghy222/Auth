using Auth.Contracts.Response;
using Auth.Features.Organizations.Contracts.Enums;

namespace Auth.Features.Organizations.Contracts.Responses
{
    public record OrganizationResponse
    {
        public required Ulid Id { get; set; }

        public required string Title { get; set; }

        public Ulid? ParentId { get; set; }
        public string? ParentTitle { get; set; }

        public OrganizationStatus Status { get; set; }

        public IEnumerable<OrganizationResponse> Children { get; set; } = [];
    }

    public record OrganizationsResponse : QueryResponse<OrganizationResponse>;
}