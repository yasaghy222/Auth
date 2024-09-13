using LanguageExt;
using Auth.Domain.Entities;
using System.Linq.Expressions;
using Auth.Contracts.Common;
using Auth.Features.Organizations.Contracts.Responses;
using Auth.Features.Organizations.Contracts.Requests;

namespace Auth.Features.Organizations.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization, Ulid>
    {
        public Task<Ulid[]> GetParentIdsAsync(Organization organization);

        public Task<Option<Organization>> FindAsync(
                       Expression<Func<Organization, bool>> expression,
                       IEnumerable<Ulid>? childrenIds,
                       CancellationToken ct);

        public Task<OrganizationsResponse> ToListByFilters(
               OrganizationFilterRequest request, CancellationToken ct);

        public Task<Option<OrganizationInfo>> GetInfoAsync(Ulid id, CancellationToken ct);
    }
}