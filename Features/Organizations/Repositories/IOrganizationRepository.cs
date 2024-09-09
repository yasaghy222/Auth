using LanguageExt;
using Auth.Domain.Entities;
using System.Linq.Expressions;
using Auth.Contracts.Common;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.Repositories
{
    public interface IOrganizationRepository : IRepository<Organization, Ulid>
    {
        public Task<Option<Organization>> FindAsync(
                       Expression<Func<Organization, bool>> expression,
                       IEnumerable<Ulid>? childrenIds,
                       CancellationToken ct);

        public Task<Option<OrganizationInfo>> GetInfoAsync(Ulid idm, CancellationToken ct);
    }
}