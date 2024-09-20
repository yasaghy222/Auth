using Auth.Domain.Entities;
using Auth.Features.Roles.Contracts.Enums;
using Auth.Features.Roles.CommandQuery.Commands.Create;
using Auth.Features.Roles.EndPoints.Create;

namespace Auth.Features.Roles.Contracts.Mappings
{
    public static class CreateMappings
    {
        public static CreateCommand MapToCommand(this RoleCreateDto dto)
        {
            return new()
            {
                Title = dto.Title,
                OrganizationId = dto.OrganizationId,
            };
        }

        public static Role MapToEntity(this CreateCommand command)
        {
            return new()
            {
                Id = Ulid.NewUlid(DateTime.UtcNow),
                Title = command.Title,
                OrganizationId = command.OrganizationId,
                Status = RoleStatus.Active
            };
        }
    }
}