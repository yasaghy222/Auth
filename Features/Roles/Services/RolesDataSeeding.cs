using Auth.Domain.Entities;
using Auth.Features.Roles.Contracts.Enums;

namespace Auth.Features.Roles.Services
{
    public static class RolesDataSeeding
    {
        public static IEnumerable<Role> InitialItems =>
        [
               new Role
                {
                    Id = Ulid.Parse("01J5NAK6SJGDKN0NJ00YX7MWXP"),
                    Title = "Admin.Auth.Service",
                    Status = RoleStatus.Active,
                    OrganizationId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8") // auth.service.id
                },
            ];
    }
}