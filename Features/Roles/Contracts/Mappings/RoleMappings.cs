using Auth.Domain.Entities;
using Auth.Features.Roles.Contracts.Responses;

namespace Auth.Features.Roles.Contracts.Mappings
{
    public static class RoleMappings
    {

        public static RoleResponse MapToResponse(
                this Role role)
        {
            return new()
            {
                Id = role.Id,
                Title = role.Title,
                OrganizationId = role.OrganizationId,
                OrganizationTitle = role.Organization?.Title ?? string.Empty,
                Status = role.Status,
            };
        }

        public static IEnumerable<RoleResponse> MapToResponse(
            this IEnumerable<Role> roles)
        {
            return roles.Select(i => i.MapToResponse());
        }
    }
}