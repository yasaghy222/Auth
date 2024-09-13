using Auth.Contracts.Request;

namespace Auth.Features.Organizations.Contracts.Requests
{
    public record IOrganizationQuery<TResponse> : IQuery<TResponse>;
}