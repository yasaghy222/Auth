using Auth.Features.Roles.CommandQuery.Commands.Update;
using Auth.Features.Roles.Contracts.Requests;
using Auth.Features.Roles.EndPoints.Update;

namespace Auth.Features.Roles.Contracts.Mappings
{
    public static class UpdateMappings
    {
        public static UpdateCommand MapToCommand(
            this RoleUpdateDto dto)
        {
            return new()
            {
                Id = dto.Id,
                Title = dto.Title,
                OrganizationId = dto.OrganizationId,
            };
        }

        public static UpdateRequest MapToRequest(
            this UpdateCommand command)
        {
            return new()
            {
                Id = command.Id,
                Title = command.Title,
            };
        }
    }
}