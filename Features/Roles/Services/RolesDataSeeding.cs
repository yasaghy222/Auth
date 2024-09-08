using Auth.Domain.Entities;
using Auth.Features.Roles.Contracts.Enums;
using Auth.Shared.Constes;

namespace Auth.Features.Roles.Services
{
    public static class RolesDataSeeding
    {
        public static IEnumerable<Role> InitialItems =>
        [
               new Role
                {
                    Id = Ulid.Parse(RoleConstes.Admin_Role_Id),
                    Title = RoleConstes.Admin_Role_Name,
                    Status = RoleStatus.Active,
                    OrganizationId = Ulid.Parse(OrganizationConstes.Auth_Service_Id)
                },
            ];
    }
}