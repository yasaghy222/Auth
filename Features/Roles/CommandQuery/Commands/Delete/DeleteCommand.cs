using Auth.Features.Roles.Contracts.Requests;
using ErrorOr;

namespace Auth.Features.Roles.CommandQuery.Commands.Delete
{
    public record DeleteCommand(Ulid Id) : IRoleCommand<ErrorOr<Deleted>>;
}