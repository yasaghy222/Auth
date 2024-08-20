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
                new Role
                {
                    Id = Ulid.Parse("01J5NANQX796T02CTDQ5X5YKQM"),
                    Title = "Admin.Accounting.Service",
                    Status = RoleStatus.Active,
                    OrganizationId = Ulid.Parse("01J5N8P83VSNQRJNJ8FD6E0M8E") // accounting.service.id
                },
                new Role
                {
                    Id = Ulid.Parse("01J5NAWMCDS3J0GFNAY7F05AJ5"),
                    Title = "Admin.RedSense.Service",
                    Status = RoleStatus.Active,
                    OrganizationId = Ulid.Parse("01J5N8QDTDVH64T5K3B9RCRB88") // redsense.service.id
                },
                new Role
                {
                    Id = Ulid.Parse("01J5Q8897YHWE25BQFJA2DY7FH"),
                    Title = "Admin.RedGuard.Update.Service",
                    Status = RoleStatus.Active,
                    OrganizationId = Ulid.Parse("01J5NB3YB6VX04GYFERCAGE5Z8") // redguard.update.service.id
                },
            ];
    }
}