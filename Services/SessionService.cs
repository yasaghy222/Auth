using Authenticate.Context;
using Authenticate.DTOs;
using Authenticate.Entities;
using FluentValidation;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.Services
{
    public class SessionService(AuthenticateContextDb db)
    {
        private readonly AuthenticateContextDb Db = db;

        private List<Session> Find(Guid userId) => [.. Db.Sessions.Where(i => i.UserId == userId)];

        public async Task<Session?> Find(SessionDto dto) => await Db.Sessions.FirstOrDefaultAsync(i => i.UserId == dto.UserId &&
            i.Platform == dto.Platform &&
             i.UniqueId == dto.UniqueId &&
             i.ExpireDate >= DateTime.UtcNow);

        public async Task<bool> Any(string token) => await Db.Sessions.AnyAsync(i => i.Token == token && i.ExpireDate >= DateTime.UtcNow);

        public async Task Add(SessionDto dto)
        {
            Session item = dto.Adapt<Session>();
            await Db.AddAsync(item);
            await Db.SaveChangesAsync();
        }

        public async Task<bool> Destroy(string token)
        {
            int effectedRow = await Db.Sessions.Where(i => i.Token == token && i.ExpireDate >= DateTime.UtcNow)
                                                                      .ExecuteUpdateAsync(setters => setters.SetProperty(i => i.ExpireDate, DateTime.UtcNow.AddDays(-1)));

            return effectedRow == 1;
        }
    }
}
