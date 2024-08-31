using Auth.Domain.Entities;

namespace Auth.Features.UserOrganizations.Services
{
    public static class UserOrganizationsDataSeeding
    {
        public static IEnumerable<UserOrganization> InitialItems =>
        [
            new UserOrganization{
                Id = Ulid.Parse("01J5Q7SMRDNHWQQTFREXJX7P00"),
                OrganizationId =Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"), // auth.service.id
                RoleId =Ulid.Parse("01J5NAK6SJGDKN0NJ00YX7MWXP"), // admin.auth.service.role.id
                UserId = Ulid.Parse("01J5Q6HDSW0J4G3SRWGJNZYJFD"), // user.id
            },
        ];
    }
}