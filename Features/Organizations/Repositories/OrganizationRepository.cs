using Auth.Data;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Data.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Auth.Features.Organizations.Contracts.Enums;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.Repositories
{
    public class OrganizationRepository(AuthDBContext db) :
        Repository<Organization, Ulid>(db),
        IOrganizationRepository
    {
        private readonly AuthDBContext _db = db;

        public async Task<Option<Organization>> FindAsync(
            Expression<Func<Organization, bool>> expression,
            IEnumerable<Ulid>? childrenIds,
            CancellationToken ct)
        {
            if (childrenIds != null)
            {
                return await _db.Organizations
                    .Include(i => i.Children.Where(i => childrenIds.Contains(i.Id)))
                    .ThenInclude(i => i.Children.Where(i => childrenIds.Contains(i.Id)))
                    .FirstOrDefaultAsync(expression, ct);
            }

            return await _db.Organizations
                .Include(i => i.Children)
                .ThenInclude(i => i.Children)
                .FirstOrDefaultAsync(expression, ct);
        }


        public async Task<Option<OrganizationInfo>> GetInfoAsync(Ulid id, CancellationToken ct)
        {
            return await _db.Organizations
                .Where(i => i.Id == id && i.Status == OrganizationStatus.Active)
                .Include(i => i.Children.Where(j => j.Status == OrganizationStatus.Active))
                .ThenInclude(i => i.Children.Where(j => j.Status == OrganizationStatus.Active))
                .Select(i => i.MapToInfo())
                .FirstOrDefaultAsync(ct);
        }
    }
}