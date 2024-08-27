using Auth.Features.Users.CommandQuery.Commands.ChangePassword;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.EndPoints.ChangePassword;
using Auth.Shared.Extensions;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class ChangePasswordMappings
    {
        public static ChangePasswordCommand MapToCommand(
            this UserChangePasswordDto dto)
        {
            return new()
            {
                Id = dto.Id,
                Password = dto.Password,
                OldPassword = dto.OldPassword,
            };
        }

        public static ChangePasswordRequest MapToRequest(
            this ChangePasswordCommand command, IHashService hashService)
        {
            return new()
            {
                Id = command.Id,
                Password = hashService.HashString(command.Password),
                OldPassword = hashService.HashString(command.OldPassword),
            };
        }
    }
}