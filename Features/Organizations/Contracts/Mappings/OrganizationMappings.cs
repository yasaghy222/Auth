using Auth.Domain.Entities;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.Contracts.Mappings
{
    public static class OrganizationMappings
    {
        private static Ulid[] GetAllChildIds(this Organization organization)
        {
            List<Ulid> childIds = organization.Children.Select(ch => ch.Id).ToList();

            foreach (Organization child in organization.Children)
            {
                childIds = [.. childIds, .. GetAllChildIds(child)];
            }
            if (organization.ParentId == null)
            {
                childIds.Add(organization.Id);
            }

            return [.. childIds];
        }

        public static OrganizationInfo MapToInfo(
            this Organization organization)
        {
            return new()
            {
                Id = organization.Id,
                Title = organization.Title,
                Chides = organization.Children.Select(MapToInfo),
                ChidesIds = organization.GetAllChildIds()
            };
        }

        public static OrganizationResponse MapToResponse(this Organization organization)
        {
            return new()
            {
                Id = organization.Id,
                Title = organization.Title,

                ParentId = organization.ParentId,
                ParentTitle = organization.Parent?.Title,

                Status = organization.Status,

                Children = organization.Children.MapToResponse(),
            };
        }

        public static IEnumerable<OrganizationResponse> MapToResponse(
            this IEnumerable<Organization> organizations)
        {
            return organizations.Select(MapToResponse);
        }
    }
}