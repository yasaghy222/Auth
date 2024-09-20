using ErrorOr;
using Auth.Features.Roles.Contracts.Requests;

namespace Auth.Features.Roles.CommandQuery.Commands.Update
{
    public record UpdateCommand : IRoleCommand<ErrorOr<Updated>>
    {
        public required Ulid Id { get; set; }
        public required string Title { get; set; }
        public required Ulid OrganizationId { get; set; }
    }
}