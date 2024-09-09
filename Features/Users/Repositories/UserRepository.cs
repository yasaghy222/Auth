using Auth.Data;
using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Enums;
using Auth.Data.Repositories;
using Auth.Shared.Extensions;
using System.Linq.Expressions;
using Auth.Contracts.Common;
using Auth.Contracts.Response;
using Microsoft.EntityFrameworkCore;
using Auth.Features.Users.Contracts.Enums;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Mappings;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Repositories
{
    public class UserRepository(AuthDBContext db) :
        Repository<User, Ulid>(db),
        IUserRepository
    {
        private readonly AuthDBContext _db = db;

        public async Task<Option<User>> FindAsync(
            Expression<Func<User, bool>> expression,
            IEnumerable<Ulid>? organizationChidesIds,
            CancellationToken ct)
        {
            if (organizationChidesIds != null)
            {
                return await _db.Users.Include(i => i.UserOrganizations
                    .Where(i => organizationChidesIds.Contains(i.OrganizationId)))
                    .FirstOrDefaultAsync(expression, ct);
            }

            return await _db.Users.Include(i => i.UserOrganizations)
                .FirstOrDefaultAsync(expression, ct);
        }


        private static Expression<Func<User, bool>> GetExpression(UserFilterRequest request)
        {
            Expression<Func<User, bool>>? expression = u =>
                u.UserOrganizations != null &&
                request.OrganizationIds.Any() &&
                request.OrganizationIds.Any(i =>
                    u.UserOrganizations.Select(x => x.OrganizationId).Contains(i));


            if (request.Ids != null && request.Ids.Any())
            {
                expression = u => request.Ids.Contains(u.Id);
            }

            if (!string.IsNullOrEmpty(request.FullName))
            {
                Expression<Func<User, bool>>? nameExpression = request.FullNameComparisonType switch
                {
                    QueryComparisonType.Contains => u => u.Name.Contains(request.FullName),
                    QueryComparisonType.NotContains => u => !u.Name.Contains(request.FullName),

                    QueryComparisonType.EqualTo => u => u.Name == request.FullName,
                    QueryComparisonType.NotEqualTo => u => u.Name != request.FullName,

                    _ => null
                };

                if (nameExpression != null)
                {
                    expression = expression == null ? nameExpression : expression.AndAlso(nameExpression);
                }
            }

            if (request.Usernames != null && request.Usernames.Any())
            {
                Expression<Func<User, bool>>? usernamesExpression = request.UsernamesComparisonType switch
                {
                    QueryComparisonType.Contains => u => request.Usernames.Contains(u.Username),
                    QueryComparisonType.NotContains => u => !request.Usernames.Contains(u.Username),

                    QueryComparisonType.EqualTo => u => request.Usernames.Any(i => i == u.Username),
                    QueryComparisonType.NotEqualTo => u => request.Usernames.Any(i => i != u.Username),
                    _ => null
                };

                if (usernamesExpression != null)
                {
                    expression = expression == null ? usernamesExpression : expression.AndAlso(usernamesExpression);
                }
            }

            if (request.Phones != null && request.Phones.Any())
            {
                Expression<Func<User, bool>>? phonesExpression = request.PhonesComparisonType switch
                {
                    QueryComparisonType.Contains => u => request.Phones.Contains(u.Phone),
                    QueryComparisonType.NotContains => u => !request.Phones.Contains(u.Phone),

                    QueryComparisonType.EqualTo => u => request.Phones.Any(i => i == u.Phone),
                    QueryComparisonType.NotEqualTo => u => request.Phones.Any(i => i != u.Phone),
                    _ => null
                };

                if (phonesExpression != null)
                {
                    expression = expression == null ? phonesExpression : expression.AndAlso(phonesExpression);
                }
            }

            if (request.IsPhoneValid != null)
            {
                Expression<Func<User, bool>> isPhoneValidExpression = u => u.IsPhoneValid == request.IsPhoneValid;
                expression = expression == null ? isPhoneValidExpression : expression.AndAlso(isPhoneValidExpression);
            }

            if (request.IsEmailValid != null)
            {
                Expression<Func<User, bool>> isEmailValidExpression = u => u.IsEmailValid == request.IsEmailValid;
                expression = expression == null ? isEmailValidExpression : expression.AndAlso(isEmailValidExpression);
            }

            if (request.Status != null)
            {
                Expression<Func<User, bool>> statusExpression = u => u.Status == request.Status;
                expression = expression == null ? statusExpression : expression.AndAlso(statusExpression);
            }

            // applies date filters
            DateFilterDto dateFilterDto = request.ToDateFilterDto();
            Expression<Func<User, bool>>? dateExpression = dateFilterDto.GetDateExpression<User>();
            if (dateExpression != null) expression.AndAlso(dateExpression);

            return expression;
        }
        private static Func<IQueryable<User>, IOrderedQueryable<User>> GetOrders(UserFilterRequest request)
        {
            Func<IQueryable<User>, IOrderedQueryable<User>> order;
            order = request.IdOrderType switch
            {
                QueryOrderType.Ascending =>
                    query => query.OrderBy(u => u.Id),

                _ => query => query.OrderByDescending(u => u.Id),
            };

            if (request.FullNameOrderType.HasValue)
            {
                order = request.FullNameOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.Name),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.Name),

                    _ => order
                };
            }

            if (request.UsernameOrderType.HasValue)
            {
                order = request.UsernameOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.Username),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.Username),

                    _ => order
                };
            }

            if (request.PhoneOrderType.HasValue)
            {
                order = request.PhoneOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.Phone),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.Phone),

                    _ => order
                };
            }

            if (request.IsPhoneValidOrderType.HasValue)
            {
                order = request.IsPhoneValidOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.IsPhoneValid),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.IsPhoneValid),

                    _ => order
                };
            }

            if (request.IsEmailValidOrderType.HasValue)
            {
                order = request.IsEmailValidOrderType switch
                {
                    QueryOrderType.Ascending => query =>
                        query.OrderBy(u => u.IsEmailValid),

                    QueryOrderType.Descending => query =>
                        query.OrderByDescending(u => u.IsEmailValid),

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

        public async Task<UsersResponse> ToListByFilters(
            UserFilterRequest request, CancellationToken ct)
        {
            static UserResponse selector(User user) => user.ToResponse();
            Expression<Func<User, bool>>? expression = GetExpression(request);
            Func<IQueryable<User>, IOrderedQueryable<User>> order = GetOrders(request);

            QueryResponse<UserResponse> queryResponse = await base.ToListAsync(
                selector, expression, order, request.PageIndex ?? 1, request.PageSize ?? 10, ct);

            return queryResponse.ToResponse();
        }

        public async Task<bool> UpdateFailedLoginInfoAsync(
                  LoginFailedRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
                i => i.Id == request.Id,
                setter =>
                    setter.SetProperty(i => i.FailedLoginAttempts, request.FailedLoginAttempts)
                    .SetProperty(i => i.AccountLockedUntil, request.AccountLockedUntil)
                    .SetProperty(i => i.Status, request.Status)
                    .SetProperty(i => i.ModifyAt, DateTime.UtcNow),
                ct);
        }

        public async Task<bool> ResetFailedLoginInfoAsync(
                  ResetFailedStatusRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
                i => i.Id == request.Id,
                setter =>
                    setter.SetProperty(i => i.FailedLoginAttempts, 0)
                    .SetProperty(i => i.AccountLockedUntil, (DateTime?)null)
                    .SetProperty(i => i.Status, UserStatus.Active)
                    .SetProperty(i => i.ModifyAt, DateTime.UtcNow),
                ct);
        }

        public async Task<bool> ChangePasswordAsync(
            ChangePasswordRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
                i => i.Id == request.Id
                    && i.Status == UserStatus.Active,
                setter =>
                    setter.SetProperty(i => i.Password, request.Password)
                    .SetProperty(i => i.ModifyAt, DateTime.UtcNow),
                ct);
        }

        public async Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
              i => i.Id == request.Id
                  && i.Status == UserStatus.Active,
              setter =>
                  setter.SetProperty(i => i.Username, request.Username)
                  .SetProperty(i => i.Name, request.Name)
                  .SetProperty(i => i.Family, request.Family)
                  .SetProperty(i => i.Phone, request.Phone)
                  .SetProperty(i => i.IsPhoneValid, false)
                  .SetProperty(i => i.Email, request.Email)
                  .SetProperty(i => i.IsEmailValid, false)
                  .SetProperty(i => i.ModifyAt, DateTime.UtcNow),
              ct);
        }
    }
}