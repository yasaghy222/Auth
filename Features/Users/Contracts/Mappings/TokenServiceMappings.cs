using System.Security.Claims;
using Auth.Shared.Extensions;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class TokenServiceMappings
    {
        public static Claim[] MapToClaims(
               this GenerateTokenRequest request)
        {
            return
            [
                new Claim(UserClaimsTypes.SessionId, request.SessionId.ToString()),
                new Claim(UserClaimsTypes.UserInfo, request.UserInfo.ToJson()),
                new Claim(UserClaimsTypes.LoginOrganizationId, request.LoginOrganizationId.ToString()),
                new Claim(UserClaimsTypes.LoginOrganizationTitle, request.LoginOrganizationTitle.ToString()),
                new Claim(UserClaimsTypes.UserOrganizations, request.UserOrganizations.ToJson()),
                new Claim(UserClaimsTypes.UserPermissions, request.Permissions.ToJson()),
            ];
        }

    }
}