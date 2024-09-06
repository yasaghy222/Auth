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
                new Claim(UserClaimNames.SessionId, request.SessionId.ToString()),
                new Claim(UserClaimNames.UserInfo, request.UserInfo.ToJson()),
                new Claim(UserClaimNames.Organizations, request.UserOrganizations.ToJson()),
                new Claim(UserClaimNames.LoginOrganizationId, request.LoginOrganizationId.ToString()),
                new Claim(UserClaimNames.LoginOrganizationTitle, request.LoginOrganizationTitle.ToString()),
                new Claim(UserClaimNames.UserOrganizations, request.UserOrganizations.ToJson()),
                new Claim(UserClaimNames.UserPermissions, request.Permissions.ToJson()),
            ];
        }

    }
}