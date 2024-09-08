using Auth.Domain.Entities;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.Contracts.Mappings
{
    public static class OrganizationMappings
    {
        public static IEnumerable<Ulid> GetAllChildIds(this Organization organization)
        {
            List<Ulid> childIds = organization.Chides.Select(ch => ch.Id).ToList();

            foreach (Organization child in organization.Chides)
            {
                childIds = [.. childIds, .. GetAllChildIds(child)];
            }
            if (organization.ParentId == null)
            {
                childIds.Add(organization.Id);
            }

            return childIds;
        }

        public static OrganizationInfo MapToInfo(
            this Organization organization)
        {
            return new()
            {
                Id = organization.Id,
                Title = organization.Title,
                Chides = organization.Chides.Select(MapToInfo),
                ChidesIds = organization.GetAllChildIds()
            };
        }
    }
}