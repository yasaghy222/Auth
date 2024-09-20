using Auth.Data;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Enums;
using Auth.Data.Repositories;
using Auth.Shared.Extensions;
using Auth.Contracts.Common;
using System.Linq.Expressions;
using Auth.Contracts.Response;
using Microsoft.EntityFrameworkCore;
using Auth.Features.Organizations.Contracts.Enums;
using Auth.Features.Organizations.Contracts.Requests;
using Auth.Features.Organizations.Contracts.Mappings;
using Auth.Features.Organizations.Contracts.Responses;

namespace Auth.Features.Organizations.Repositories
{
    public class OrganizationRepository(AuthDBContext db) :
        Repository<Organization, Ulid>(db),
        IOrganizationRepository
    {
        private readonly AuthDBContext _db = db;

        public async Task<Ulid[]> GetParentIdsAsync(Organization organization)
        {
            List<Ulid> parentIds = [organization.Id];

            Organization? currentOrganization = organization;

            // Loop through parents until the root is reached
            while (currentOrganization?.Parent != null)
            {
                if (currentOrganization.ParentId != null)
                {
                    parentIds.Add((Ulid)currentOrganization.ParentId);
                }

                // Fetch the parent organization from the database
                currentOrganization = await _db.Organizations
                    .Include(o => o.Parent) // Include the parent for recursive lookup
                    .FirstOrDefaultAsync(o => o.Id == currentOrganization.Parent.Id);
            }

            return [.. parentIds];
        }

        public async Task<Option<Organization>> FindAsync(
            Expression<Func<Organization, bool>> expression,
            IEnumerable<Ulid>? childrenIds,
            CancellationToken ct)
        {
            if (childrenIds != null)
            {
                return await _db.Organizations
                    .Include(i => i.Parent)
                    .Include(i => i.Children.Where(i => childrenIds.Contains(i.Id)))
                    .ThenInclude(i => i.Children.Where(i => childrenIds.Contains(i.Id)))
                    .ThenInclude(i => i.Children.Where(i => childrenIds.Contains(i.Id)))
                    .ThenInclude(i => i.Children.Where(i => childrenIds.Contains(i.Id)))
                    .FirstOrDefaultAsync(expression, ct);
            }

            return await _db.Organizations
                .Include(i => i.Parent)
                .Include(i => i.Children)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Children)
                .FirstOrDefaultAsync(expression, ct);
        }

        private static Expression<Func<Organization, bool>> GetExpression(OrganizationFilterRequest request)
        {
            Expression<Func<Organization, bool>>? expression = i =>
                request.Ids.Contains(i.Id);

            if (!string.IsNullOrEmpty(request.Title))
            {
                Expression<Func<Organization, bool>>? titleExpression = request.TittleComparisonType switch
                {
                    QueryComparisonType.Contains => i => i.Title.Contains(request.Title),
                    QueryComparisonType.NotContains => i => !i.Title.Contains(request.Title),

                    QueryComparisonType.EqualTo => i => i.Title == request.Title,
                    QueryComparisonType.NotEqualTo => i => i.Title != request.Title,

                    _ => null
                };

                if (titleExpression != null)
                {
                    expression = expression == null ? titleExpression : expression.AndAlso(titleExpression);
                }
            }

            if (request.Status != null)
            {
                Expression<Func<Organization, bool>> statusExpression = i => i.Status == request.Status;
                expression = expression == null ? statusExpression : expression.AndAlso(statusExpression);
            }

            // applies date filters
            DateFilterDto dateFilterDto = request.MapToDateFilterDto();
            Expression<Func<Organization, bool>>? dateExpression = dateFilterDto.GetDateExpression<Organization>();
            if (dateExpression != null) expression.AndAlso(dateExpression);

            return expression;
        }

        private static Func<IQueryable<Organization>, IOrderedQueryable<Organization>>
             GetOrders(OrganizationFilterRequest request)
        {
            Func<IQueryable<Organization>, IOrderedQueryable<Organization>> order;
            order = request.IdOrderType switch
            {
                QueryOrderType.Ascending =>
                    query => query.OrderBy(u => u.Id),

                _ => query => query.OrderByDescending(u => u.Id),
            };

            if (request.TitleOrderType.HasValue)
            {
                order = request.TitleOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(i => i.Title),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(i => i.Title),

                    _ => order
                };
            }

            if (request.StatusOrderType.HasValue)
            {
                order = request.StatusOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.Status),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.Status),

                    _ => order
                };
            }

            if (request.CreateAtOrderType.HasValue)
            {
                order = request.CreateAtOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.CreateAt),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.CreateAt),

                    _ => order
                };
            }

            return order;
        }

        public async Task<OrganizationsResponse> ToListByFilters(
            OrganizationFilterRequest request, CancellationToken ct)
        {
            static OrganizationResponse selector(Organization organization) => organization.MapToResponse();
            Expression<Func<Organization, bool>>? expression = GetExpression(request);
            Func<IQueryable<Organization>, IOrderedQueryable<Organization>> order = GetOrders(request);

            IQueryable<Organization> query = base.ToQueryable(expression)
                .Include(i => i.Children)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Children)
                .ThenInclude(i => i.Children);

            QueryResponse<OrganizationResponse> queryResponse = await base.ToListAsync(
                query, selector, expression, order, request.PageIndex ?? 1, request.PageSize ?? 10, ct);

            return queryResponse.MapToResponse();
        }

        public async Task<Option<OrganizationInfo>> GetInfoAsync(Ulid id, CancellationToken ct)
        {
            return await _db.Organizations
                .Where(i => i.Id == id && i.Status == OrganizationStatus.Active)
                .Include(i => i.Children.Where(j => j.Status == OrganizationStatus.Active))
                .ThenInclude(i => i.Children.Where(j => j.Status == OrganizationStatus.Active))
                .Select(i => i.MapToInfo(default))
                .FirstOrDefaultAsync(ct);
        }

        public async Task<bool> UpdateAsync(
         UpdateRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
              i => i.Id == request.Id,
              setter =>
                  setter.SetProperty(i => i.Title, request.Title)
                  .SetProperty(i => i.ParentId, request.ParentId)
                  .SetProperty(i => i.ModifyAt, DateTime.UtcNow),
              ct);
        }
    }
}