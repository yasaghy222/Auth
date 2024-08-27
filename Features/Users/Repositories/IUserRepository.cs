using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Responses;

namespace Auth.Features.Users.Repositories
{
    public interface IUserRepository : IRepository<User, Ulid>
    {
        public Task<UsersResponse> ToListByFilters(UserFilterRequest request, CancellationToken ct);

        public Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct);

        public Task<bool> ChangePasswordAsync(
           ChangePasswordRequest request, CancellationToken ct);
    }
}