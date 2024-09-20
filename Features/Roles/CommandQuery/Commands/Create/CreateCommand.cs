using Auth.Features.Roles.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Roles.CommandQuery.Commands.Create
{
    public record CreateCommand : IRoleCommand<ErrorOr<Ulid>>
    {
        public required string Title { get; set; }
        public required Ulid OrganizationId { get; set; }
    }
}