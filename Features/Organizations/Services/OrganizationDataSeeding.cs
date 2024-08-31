using Auth.Domain.Entities;
using Auth.Features.Organizations.Contracts.Enums;

namespace Auth.Features.Organizations.Services
{
    public static class OrganizationDataSeeding
    {
        public static IEnumerable<Organization> InitialItems =>
         [
                new Organization
                {
                    Id = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                    Title = "Auth.Service",
                    Status = OrganizationStatus.Active,
                    ParentId = null,
                },
                new Organization
                {
                    Id = Ulid.Parse("01J5N8P83VSNQRJNJ8FD6E0M8E"),
                    Title = "Accounting.Service",
                    Status = OrganizationStatus.Active,
                    ParentId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                },
                new Organization
                {
                    Id = Ulid.Parse("01J5N8QDTDVH64T5K3B9RCRB88"),
                    Title = "RedSense.Service",
                    Status = OrganizationStatus.Active,
                    ParentId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8") // Set parent relationship
                },
                new Organization
                {
                    Id = Ulid.Parse("01J5N3BYB6VXQ4GYFERCAGE5Z8"),
                    Title = "RedGuard.Update.Service",
                    Status = OrganizationStatus.Active,
                    ParentId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8") // Set parent relationship
                }
            ];
    }
}