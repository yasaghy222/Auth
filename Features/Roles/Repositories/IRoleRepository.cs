using Auth.Domain.Entities;
using Auth.Contracts.Common;
using Auth.Features.Roles.Contracts.Requests;
using Auth.Features.Roles.Contracts.Responses;

namespace Auth.Features.Roles.Repositories
{
    public interface IRoleRepository : IRepository<Role, Ulid>
    {
        public Task<RolesResponse> ToListByFilters(
           RoleFilterRequest request, CancellationToken ct);

        public Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct);
    }
}