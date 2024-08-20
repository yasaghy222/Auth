using Auth.Data;
using Auth.Domain.Entities;
using Auth.Data.Repositories;
using Auth.Features.Users.Contracts.Requests;

namespace Auth.Features.Users.Repositories
{
    public class UserRepository(AuthDBContext db) :
        Repository<User, Ulid>(db),
        IUserRepository
    {
        public Task<bool> UpdateAsync(UpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}