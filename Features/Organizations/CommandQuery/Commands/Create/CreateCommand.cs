using Auth.Features.Organizations.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Organizations.CommandQuery.Commands.Create
{
    public record CreateCommand : IOrganizationCommand<ErrorOr<Ulid>>
    {
        public required string Title { get; set; }
        public required Ulid ParentId { get; set; }
    }
}