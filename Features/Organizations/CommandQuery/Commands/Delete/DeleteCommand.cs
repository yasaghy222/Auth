using Auth.Features.Organizations.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Organizations.CommandQuery.Commands.Delete
{
    public record DeleteCommand(Ulid Id) : IOrganizationCommand<ErrorOr<Deleted>>;
}