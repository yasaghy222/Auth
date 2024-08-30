using LanguageExt;
using Auth.Domain.Entities;
using Auth.Contracts.Common;
using System.Linq.Expressions;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Repositories
{
    public interface IUserRepository : IRepository<User, Ulid>
    {
        public Task<Option<User>> FindAsync(
                  Expression<Func<User, bool>> expression,
                  IEnumerable<Ulid>? organizationChidesIds,
                  CancellationToken ct);

        public Task<UsersResponse> ToListByFilters(
            UserFilterRequest request, CancellationToken ct);

        public Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct);

        public Task<bool> UpdateFailedLoginInfoAsync(
                  LoginFailedRequest request, CancellationToken ct);

        public Task<bool> ChangePasswordAsync(
           ChangePasswordRequest request, CancellationToken ct);
    }
}