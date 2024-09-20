using Auth.Data;
using Auth.Domain.Entities;
using Auth.Data.Repositories;
using Auth.Features.Roles.Contracts.Requests;

namespace Auth.Features.Roles.Repositories
{
    public class RoleRepository(AuthDBContext db) :
        Repository<Role, Ulid>(db),
        IRoleRepository
    {
        private readonly AuthDBContext _db = db;

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