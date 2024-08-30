using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Features.Organizations.Contracts.Responses;
using LanguageExt;

namespace Auth.Features.Organizations.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization, Ulid>
    {
        public Task<Option<OrganizationInfo>> GetInfoAsync(Ulid idm, CancellationToken ct);
    }
}