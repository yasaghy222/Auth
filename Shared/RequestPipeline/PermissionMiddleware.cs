using ErrorOr;
using FastEndpoints;
using System.Security.Claims;
using Auth.Shared.Extensions;
using Auth.Shared.CustomErrors;
using Auth.Features.Users.Contracts.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Auth.Contracts.Common;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Shared.RequestPipeline
{
    public class PermissionMiddleware(IUserClaimsInfo userClaimsInfo)
        : IAuthorizationMiddlewareResultHandler
    {
        private readonly IUserClaimsInfo _userClaimsInfo = userClaimsInfo;

        private static ErrorOr<Ulid[]> GetUserPermissions(HttpContext context)
        {
            ClaimsPrincipal user = context.User;
            if (!user.Identity?.IsAuthenticated ?? false)
            {
                return Error.Unauthorized();
            }

            Claim? userPermissionsClaim = user.Claims
                .FirstOrDefault(i => i.Type == UserClaimsTypes.UserPermissions);

            if (userPermissionsClaim == null)
            {
                return GlobalErrors.InvalidToken();
            }

            return userPermissionsClaim?.Value.FromJson<Ulid[]>() ?? [];
        }

        private static ErrorOr<Ulid[]> GetRequirePermissions(HttpContext context)
        {
            Endpoint? endpoint = context.GetEndpoint();

            if (endpoint == null)
            {
                return GlobalErrors.InvalidToken();
            }

            EndpointDefinition? endpointDefinition = endpoint?
                .Metadata.GetMetadata<EndpointDefinition>();

            if (endpointDefinition == null)
            {
                return GlobalErrors.InvalidToken();
            }

            bool allowAnyPermission = endpointDefinition
               ?.AllowAnyPermission ?? false;

            if (!allowAnyPermission)
            {
                return Array.Empty<Ulid>();
            }

            return endpointDefinition?.AllowedPermissions
                ?.Select(i => Ulid.Parse(i))?.ToArray() ?? [];
        }

        private void FillClaimsInfo(IEnumerable<Claim> claims)
        {
            _userClaimsInfo.UserInfo = claims
                .Single(i => i.Type == UserClaimsTypes.UserInfo)
                .Value.FromJson<UserInfo>();

            _userClaimsInfo.SessionId = claims
                .Single(i => i.Type == UserClaimsTypes.SessionId).Value;

            _userClaimsInfo.Permissions = claims
                .Single(i => i.Type == UserClaimsTypes.UserPermissions)
                .Value.FromJson<IEnumerable<Ulid>>();

            _userClaimsInfo.UserOrganizations = claims
                .Single(i => i.Type == UserClaimsTypes.UserOrganizations)
                .Value.FromJson<IEnumerable<UserOrganizationInfo>>();

            _userClaimsInfo.LoginOrganizationId = Ulid.Parse(claims
                .Single(i => i.Type == UserClaimsTypes.LoginOrganizationId).Value);

            _userClaimsInfo.LoginOrganizationTitle = claims
                .Single(i => i.Type == UserClaimsTypes.LoginOrganizationTitle).Value;
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context,
            AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            ErrorOr<Ulid[]> requiredPermissions = GetRequirePermissions(context);
            if (requiredPermissions.IsError)
            {
                await context.Response.SendForbiddenAsync();
                return;
            }

            FillClaimsInfo(context.User.Claims);

            if (requiredPermissions.Value.Length == 0)
            {
                await next.Invoke(context);
                return;
            }

            ErrorOr<Ulid[]> userPermissions = GetUserPermissions(context);
            if (userPermissions.IsError)
            {
                await context.Response.SendForbiddenAsync();
                return;
            }

            bool hasPermission = userPermissions.Value.Any(i => requiredPermissions.Value.Contains(i));
            if (!hasPermission)
            {
                await context.Response.SendForbiddenAsync();
                return;
            }

            await next.Invoke(context);
            return;
        }
    }
}