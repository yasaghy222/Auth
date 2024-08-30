using Auth.Domain.Entities;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.UserOrganizations.Contracts.Mappings
{
    public static class UserOrganizationMappings
    {
        public static UserOrganizationInfo MapToInfo(
            this UserOrganization userOrganization)
        {
            return new()
            {
                OrganizationId = userOrganization.OrganizationId,
                OrganizationTitle = userOrganization
                    .Organization?.Title ?? string.Empty,

                RoleId = userOrganization.RoleId,
                RoleTitle = userOrganization.Role?.Title ?? string.Empty,
            };
        }

        public static IEnumerable<UserOrganizationInfo> MapToInfo(
            this IEnumerable<UserOrganization> userOrganizations)
        {
            return userOrganizations.Select(MapToInfo);
        }
    }
}