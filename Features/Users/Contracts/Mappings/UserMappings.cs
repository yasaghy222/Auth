using Auth.Domain.Entities;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class UserMappings
    {
        public static UserInfo MapToInfo(this User user)
        {
            return new()
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Family = user.Family,
                Email = user.Email,
                Phone = user.Phone,
            };
        }

    }
}