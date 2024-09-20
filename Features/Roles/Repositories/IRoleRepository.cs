using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Features.Roles.Contracts.Requests;

namespace Auth.Features.Roles.Repositories
{
    public interface IRoleRepository : IRepository<Role, Ulid>
    {
        public Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct);
    }
}