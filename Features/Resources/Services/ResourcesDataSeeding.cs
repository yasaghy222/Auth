using Auth.Domain.Entities;

namespace Auth.Features.Resources.Services
{
    public static class ResourcesDataSeeding
    {
        public static IEnumerable<Resource> InitialItems =>
        [
               new Resource
                {
                    Id = Ulid.Parse("01J7116S6NREKY0K41KXX8RJG9"),
                    OrganizationId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                    GroupId = Ulid.Parse("01J712NGSNRS456XTQQW6HWJNZ"),
                    Title = "Auth.User.Create",
                    Url = "/user"
                },
                 new Resource
                {
                    Id = Ulid.Parse("01J712XPPNQQYTS5M07S5ACW5Q"),
                    OrganizationId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                    GroupId = Ulid.Parse("01J712NGSNRS456XTQQW6HWJNZ"),
                    Title = "Auth.User.ListByFilters",
                    Url = "/user/list/filter"
                },
            ];
    }
}