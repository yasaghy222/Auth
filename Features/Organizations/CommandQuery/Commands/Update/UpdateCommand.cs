using ErrorOr;
using Auth.Features.Organizations.Contracts.Requests;

namespace Auth.Features.Organizations.CommandQuery.Commands.Update
{
    public record UpdateCommand
        : IOrganizationCommand<ErrorOr<Updated>>
    {
        public required Ulid Id { get; set; }
        public required string Title { get; set; }
        public required Ulid ParentId { get; set; }
    }
}