using Auth.Features.Users.EndPoints.Update;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.CommandQuery.Commands.Update;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class UpdateMappings
    {
        public static UpdateCommand MapToCommand(
            this UserUpdateDto dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name ?? string.Empty,
                Family = dto.Family ?? string.Empty,
                Username = dto.Username ?? string.Empty,
                Phone = dto.Phone,
                Email = dto.Email,
            };
        }

        public static UpdateRequest MapToRequest(
            this UpdateCommand command)
        {
            return new()
            {
                Id = command.Id,
                Name = command.Name,
                Family = command.Family,
                Username = command.Username,
                Phone = command.Phone,
                Email = command.Email,
            };
        }
    }
}