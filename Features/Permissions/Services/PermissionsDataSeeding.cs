using Auth.Domain.Entities;

namespace Auth.Features.Permissions.Services
{
    public static class PermissionsDataSeeding
    {
        public static IEnumerable<Permission> InitialItems =>
        [
               new Permission
                {
                    Id = Ulid.Parse("01J7112CV8TM060R06NRM6CBS2"),
                    RoleId =Ulid.Parse("01J5NAK6SJGDKN0NJ00YX7MWXP"),
                    ResourceId = Ulid.Parse("01J7116S6NREKY0K41KXX8RJG9")
                },
                new Permission
                {
                    Id = Ulid.Parse("01J7131S983EM4KMR5S55FPWE6"),
                    RoleId =Ulid.Parse("01J5NAK6SJGDKN0NJ00YX7MWXP"),
                    ResourceId = Ulid.Parse("01J712XPPNQQYTS5M07S5ACW5Q")
                },
            ];
    }
}