using Auth.Domain.Entities;
using Auth.Shared.Extensions;
using Auth.Features.Users.Contracts.Enums;
using Auth.Shared.Constes;

namespace Auth.Features.Users.Services
{
    public static class UsersDataSeeding
    {
        public static IEnumerable<User> GetInitialItems(IHashService hashService) =>
        [
            new User
            {
                Id = Ulid.Parse(UserConstes.Admin_Id),
                Name = RoleConstes.Admin_Role_Name,
                Family = string.Empty,
                Password = hashService.HashString(UserConstes.Admin_Password),
                Username = UserConstes.Admin_Username,
                Status = UserStatus.Active,
            },
        ];
    }
}