using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;
using Auth.Features.Users.EndPoints.UpdateProfile;
using Auth.Features.Users.CommandQuery.Commands.UpdateProfile;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class UpdateProfileMappings
    {
        public static UpdateProfileCommand MapToCommand(
            this UserUpdateProfileDto dto, UserInfo? userInfo)
        {
            return new()
            {
                Name = dto.Name ?? userInfo?.Name ?? string.Empty,
                Family = dto.Family ?? userInfo?.Family ?? string.Empty,
                Username = dto.Username ?? userInfo?.Username ?? string.Empty,
                Phone = dto.Phone,
                Email = dto.Email,
            };
        }

        public static UpdateRequest MapToRequest(
            this UpdateProfileCommand command, Ulid userId)
        {
            return new()
            {
                Id = userId,
                Name = command.Name,
                Family = command.Family,
                Username = command.Username,
                Phone = command.Phone,
                Email = command.Email,
            };
        }
    }
}