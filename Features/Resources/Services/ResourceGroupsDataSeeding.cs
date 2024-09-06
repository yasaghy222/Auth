using Auth.Domain.Entities;

namespace Auth.Features.Resources.Services
{
    public static class ResourceGroupsDataSeeding
    {
        public static IEnumerable<ResourceGroup> InitialItems =>
        [
               new ResourceGroup
                {
                    Id = Ulid.Parse("01J712NGSNRS456XTQQW6HWJNZ"),
                    OrganizationId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                    Title = "Auth.Users",
                    Order = 1,
                },
                 new ResourceGroup
                {
                    Id = Ulid.Parse("01J712TRTE84VCNAZZAVK63MEF"),
                    OrganizationId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                    Title = "Auth.Organizations",
                    Order = 2,
                },
        ];
    }
}