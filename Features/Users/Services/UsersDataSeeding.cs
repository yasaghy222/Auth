using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Auth.Features.Roles.Contracts.Enums;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Roles.Services
{
    public static class UsersDataSeeding
    {
        public static async Task ApplyInitialDatas(this DbSet<User> table)
        {
            IEnumerable<User> initialItems = [
                new User
                {
                    Id = Ulid.Parse("01J5NAZXXVEVWXFWMJVSQNN048"),
                    Title = "Admin.Auth.Service",
                    Status = UserStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    OrganizationId = Ulid.Parse("01J5N8HWG80H4Y3T6WRQ2PGBQ8") // auth.service.id
                },
                new User
                {
                    Id = Ulid.Parse("01J5NANQX796T02CTDQ5X5YKQM"),
                    Title = "Admin.Accounting.Service",
                    Status = RoleStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    OrganizationId = Ulid.Parse("01J5N8P83VSNQRJNJ8FD6E0M8E") // accounting.service.id
                },
                 new User
                {
                    Id = Ulid.Parse("01J5NAWMCDS3J0GFNAY7F05AJ5"),
                    Title = "Admin.RedSense.Service",
                    Status = RoleStatus.Active,
                    CreateAt = DateTime.UtcNow,
                    OrganizationId = Ulid.Parse("01J5N8QDTDVH64T5K3B9RCRB88") // accounting.service.id
                },
            ];
            IEnumerable<Ulid> initialIds = initialItems.Select(i => i.Id);

            if (table.Any(i => initialIds.Contains(i.Id)))
                await table.AddRangeAsync(initialItems);
        }
    }
}