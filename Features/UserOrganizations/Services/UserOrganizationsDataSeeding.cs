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
            new UserOrganization{
                Id = Ulid.Parse("01J5Q7YRYDHJWF699996A6G8HD"),
                OrganizationId =Ulid.Parse("01J5N8P83VSNQRJNJ8FD6E0M8E"), // accounting.service.id
                RoleId =Ulid.Parse("01J5NANQX796T02CTDQ5X5YKQM"), // admin.accounting.service.role.id
                UserId = Ulid.Parse("01J5Q6JNY35P6T0SKDHJ36K2XD"), // user.id
            },
            new UserOrganization{
                Id = Ulid.Parse("01J5Q82A0GDC2V6XMT925PQGH1"),
                OrganizationId =Ulid.Parse("01J5N8QDTDVH64T5K3B9RCRB88"), // redsense.service.id
                RoleId =Ulid.Parse("01J5NAWMCDS3J0GFNAY7F05AJ5"), // admin.redsense.service.role.id
                UserId = Ulid.Parse("01J5Q846WB22M41TEMAPY8P6VF"), // user.id
            },
            new UserOrganization{
                Id = Ulid.Parse("01J5Q84FQEQ83NF774VR7BYWN9"),
                OrganizationId =Ulid.Parse("01J5NB3YB6VX04GYFERCAGE5Z8"), // redguard.update.service.id
                RoleId =Ulid.Parse("01J5Q8897YHWE25BQFJA2DY7FH"), // admin.redguard.update.service.role.id
                UserId = Ulid.Parse("01J5Q86APSM2V5703AH2N0QDVY"), // user.id
            }
        ];
    }
}