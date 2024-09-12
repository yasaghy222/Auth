using Auth.Domain.Entities;
using Auth.Shared.Constes;

namespace Auth.Features.UserOrganizations.Services
{
    public static class UserOrganizationsDataSeeding
    {
        public static IEnumerable<UserOrganization> InitialItems =>
        [
            new UserOrganization{
                Id = Ulid.Parse("01J5Q7SMRDNHWQQTFREXJX7P00"),
                OrganizationId =Ulid.Parse(OrganizationConstes.Auth_Service_Id),
                RoleId =Ulid.Parse(RoleConstes.Admin_Role_Id),
                UserId = Ulid.Parse(UserConstes.Admin_Id),
            },
        ];
    }
}