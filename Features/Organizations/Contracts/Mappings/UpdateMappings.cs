using Auth.Features.Organizations.CommandQuery.Commands.Update;
using Auth.Features.Organizations.Contracts.Requests;
using Auth.Features.Organizations.EndPoints.Update;

namespace Auth.Features.Organizations.Contracts.Mappings
{
    public static class UpdateMappings
    {
        public static UpdateCommand MapToCommand(
            this OrganizationUpdateDto dto)
        {
            return new()
            {
                Id = dto.Id,
                Title = dto.Title,
                ParentId = dto.ParentId,
            };
        }

        public static UpdateRequest MapToRequest(
            this UpdateCommand command)
        {
            return new()
            {
                Id = command.Id,
                Title = command.Title,
                ParentId = command.ParentId,
            };
        }
    }
}