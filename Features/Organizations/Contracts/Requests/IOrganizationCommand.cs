using Auth.Contracts.Request;

namespace Auth.Features.Organizations.Contracts.Requests
{
    public record IOrganizationCommand<TResponse> : ICommand<TResponse>;
}