using LanguageExt;
using Auth.Domain.Entities;
using System.Linq.Expressions;
using Auth.Contracts.Common;

namespace Auth.Features.Roles.Repositories
{
    public interface IRoleRepository : IRepository<Role, Ulid>
    {
    }
}