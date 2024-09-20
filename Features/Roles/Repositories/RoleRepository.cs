using Auth.Data;
using Auth.Domain.Entities;
using Auth.Data.Repositories;

namespace Auth.Features.Roles.Repositories
{
    public class RoleRepository(AuthDBContext db) :
        Repository<Role, Ulid>(db),
        IRoleRepository
    {
        private readonly AuthDBContext _db = db;

    }
}