using Auth.Domain.Entities;
using Auth.Features.Organizations.Contracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace Auth.Features.Organizations.Services
{
    public static class OrganizationDataSeeding
    {
        public static async Task ApplyInitialDatas(this DbSet<Organization> table)
        {
            IEnumerable<Organization> initialItems = [
                new Organization
                {
                    Id = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8"),
                    Title = "Auth.Service",
                    Status = OrganizationStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    ParentId = null,
                },
                new Organization
                {
                    Id = Ulid.Parse("01J5N8P83VSNQRJNJ8FD6E0M8E"),
                    Title = "Accounting.Service",
                    Status = OrganizationStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    ParentId = null,
                },
                new Organization
                {
                    Id = Ulid.Parse("01J5N8QDTDVH64T5K3B9RCRB88"),
                    Title = "RedSense.Service",
                    Status = OrganizationStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    ParentId = null,
                },
                new Organization
                {
                    Id = Ulid.Parse("01J5NB3YB6VX04GYFERCAGE5Z8"),
                    Title = "RedGuard.Update.Service",
                    Status = OrganizationStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    ParentId = null,
                }
            ];
            IEnumerable<Ulid> initialIds = initialItems.Select(i => i.Id);

            if (table.Any(i => initialIds.Contains(i.Id)))
                await table.AddRangeAsync(initialItems);
        }
    }
}