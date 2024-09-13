using Auth.Domain.Entities;
using Auth.Features.Organizations.Contracts.Enums;
using Auth.Features.Organizations.EndPoints.Create;
using Auth.Features.Organizations.CommandQuery.Commands.Create;

namespace Auth.Features.Organizations.Contracts.Mappings
{
    public static class CreateMappings
    {
        public static CreateCommand MapToCommand(this OrganizationCreateDto dto)
        {
            return new()
            {
                Title = dto.Title,
                ParentId = dto.ParentId,
            };
        }

        public static Organization MapToEntity(this CreateCommand command)
        {
            return new()
            {
                Id = Ulid.NewUlid(DateTime.UtcNow),
                Title = command.Title,
                ParentId = command.ParentId,
                Status = OrganizationStatus.Active
            };
        }
    }
}