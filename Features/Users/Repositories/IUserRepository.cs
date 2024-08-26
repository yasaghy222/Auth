using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Features.Users.Contracts.Requests;

namespace Auth.Features.Users.Repositories
{
    public interface IUserRepository : IRepository<User, Ulid>
    {
        public Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct);

        public Task<bool> ChangePasswordAsync(
           ChangePasswordRequest request, CancellationToken ct);
    }
}