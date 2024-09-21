using Auth.Data;
using Auth.Domain.Entities;
using Auth.Data.Repositories;
using Auth.Features.Roles.Contracts.Requests;
using Auth.Features.Roles.Contracts.Responses;
using Auth.Features.Roles.Contracts.Mappings;
using System.Linq.Expressions;
using Auth.Contracts.Response;
using Auth.Contracts.Enums;
using Auth.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Features.Roles.Repositories
{
    public class RoleRepository(AuthDBContext db) :
        Repository<Role, Ulid>(db),
        IRoleRepository
    {
        private readonly AuthDBContext _db = db;

        private static Expression<Func<Role, bool>> GetExpression(RoleFilterRequest request)
        {
            Expression<Func<Role, bool>>? expression =
                i => i.OrganizationId == request.OrganizationId;

            if (request.Ids != null && request.Ids.Any())
            {
                expression = i => request.Ids.Contains(i.Id);
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                Expression<Func<Role, bool>>? titleExpression = request.TittleComparisonType switch
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
                Expression<Func<Role, bool>> statusExpression = i => i.Status == request.Status;
                expression = expression == null ? statusExpression : expression.AndAlso(statusExpression);
            }

            return expression;
        }

        private static Func<IQueryable<Role>, IOrderedQueryable<Role>> GetOrders(RoleFilterRequest request)
        {
            Func<IQueryable<Role>, IOrderedQueryable<Role>> order;
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

            return order;
        }

        public async Task<RolesResponse> ToListByFilters(
               RoleFilterRequest request, CancellationToken ct)
        {
            static RoleResponse selector(Role role) => role.MapToResponse();
            Expression<Func<Role, bool>>? expression = GetExpression(request);
            Func<IQueryable<Role>, IOrderedQueryable<Role>> order = GetOrders(request);

            IQueryable<Role> query = base.ToQueryable(expression)
                .Include(i => i.Organization);

            QueryResponse<RoleResponse> queryResponse = await base.ToListAsync(
                query, selector, expression, order, pageIndex: 1, pageSize: 100, ct);

            return new() { Items = queryResponse.Items };
        }

        public async Task<bool> UpdateAsync(
             UpdateRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
              i => i.Id == request.Id,
              setter =>
                  setter.SetProperty(i => i.Title, request.Title)
                  .SetProperty(i => i.ModifyAt, DateTime.UtcNow),
              ct);
        }
    }
}