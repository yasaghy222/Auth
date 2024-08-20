using Auth.Domain.Entities;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.CommandQuery.Commands.Create;
using Auth.Features.Users.EndPoints.Create;
using Auth.Shared.Extensions;

namespace Auth.Features.Users.Contracts.Mappings
{
    public static class CreateMappings
    {
        public static CreateCommand MapToCommand(this UserCreateDto dto)
        {
            return new()
            {
                Name = dto.Name,
                Family = dto.Family,
                Username = dto.Username,
                Phone = dto.Phone,
                Email = dto.Email,
                Password = dto.Password,
            };
        }

        public static User MapToEntity(this CreateCommand command)
        {
            return new()
            {
                Id = Ulid.NewUlid(DateTime.UtcNow),
                Name = command.Name,
                Family = command.Family,
                Username = command.Username,
                Phone = command.Phone,
                Email = command.Email,
                Password = command.Password.ToHashString(),
                Status = UserStatus.Active
            };
        }
    }
}