using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Services
{
    public static class UsersDataSeeding
    {
        public static IEnumerable<User> GetInitialItems(IHashService hashService) =>
        [
            new User
            {
                Id = Ulid.Parse("01J5Q6HDSW0J4G3SRWGJNZYJFD"),
                Name = "Admin",
                Family = "Auth.Service",
                Password = hashService.HashString("AdminAuth@123"),
                Username = "AdminAuthService1",
                Status = UserStatus.Active,
            },
            new User
            {
                Id = Ulid.Parse("01J5Q6JNY35P6T0SKDHJ36K2XD"),
                Name = "Admin",
                Family = "Accounting.Service",
                Password =hashService.HashString( "AdminAccounting@123"),
                Username = "AdminAccountingService1",
                Status = UserStatus.Active,
            },
            new User
            {
                Id = Ulid.Parse("01J5Q846WB22M41TEMAPY8P6VF"),
                Name = "Admin",
                Family = "RedSense.Service",
                Password =hashService.HashString( "AdminRedSense@123"),
                Username = "AdminRedSenseService1",
                Status = UserStatus.Active,
            },
            new User
            {
                Id = Ulid.Parse("01J5Q86APSM2V5703AH2N0QDVY"),
                Name = "Admin",
                Family = "RedGuard.Update.Service",
                Password = hashService.HashString("AdminRedGuardUpdate@123"),
                Username = "AdminRedGuardUpdateService1",
                Status = UserStatus.Active,
            },
        ];
    }
}