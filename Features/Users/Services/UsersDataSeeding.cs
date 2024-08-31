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
        ];
    }
}