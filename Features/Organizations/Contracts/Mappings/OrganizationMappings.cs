using Auth.Domain.Entities;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.Contracts.Mappings
{
    public static class OrganizationMappings
    {
        public static IEnumerable<Ulid> GetAllChildIds(this Organization organization)
        {
            IEnumerable<Ulid> childIds = organization.Chides.Select(ch => ch.Id);

            foreach (Organization child in organization.Chides)
            {
                childIds = childIds.Concat(GetAllChildIds(child));
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