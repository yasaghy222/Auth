using Auth.Data;
using Auth.Domain.Entities;
using Auth.Data.Repositories;
using Auth.Features.Users.Contracts.Requests;
using Auth.Features.Users.Contracts.Enums;

namespace Auth.Features.Users.Repositories
{
    public class UserRepository(AuthDBContext db) :
        Repository<User, Ulid>(db),
        IUserRepository
    {
        public async Task<bool> ChangePasswordAsync(
            ChangePasswordRequest request, CancellationToken ct)
        {
            return await base.EditAsync(
                i => i.Id == request.Id
                    && i.Password == request.OldPassword
                    && i.Status == UserStatus.Active,
                setter => setter.SetProperty(i => i.Password,
                    request.Password),
                ct);
        }

        public Task<bool> UpdateAsync(
            UpdateRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}