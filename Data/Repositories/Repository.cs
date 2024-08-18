using LanguageExt;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Auth.Contracts.Common;
using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Data.Repositories
{
    public class Repository<TEntity, TId>(AuthContext db) : RepositoryBase<TEntity, TId> where TEntity : class,
      IIdentityAggregate<TId>
    {
        public override List<TEntity> ToList() => [.. db.Set<TEntity>().AsNoTracking()];
        public override List<TEntity> ToList(Expression<Func<TEntity, bool>> condition) => [.. db.Set<TEntity>().Where(condition).AsNoTracking()];

        public override async Task<List<TEntity>> ToListAsync(CancellationToken ct) => await db.Set<TEntity>().AsNoTracking().ToListAsync(ct);
        public override async Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> condition, CancellationToken ct) => await db.Set<TEntity>().Where(condition).AsNoTracking().ToListAsync(ct);


        public override Option<TEntity> Find(TId id) => db.Set<TEntity>().FirstOrDefault(i => i.Id == null || i.Id.Equals(id));
        public override Option<TEntity> Find(Expression<Func<TEntity, bool>> expression) => db.Set<TEntity>().FirstOrDefault(expression);

        public override async Task<Option<TEntity>> FindAsync(TId id, CancellationToken ct) => await db.Set<TEntity>().FirstOrDefaultAsync(i => i.Id == null || i.Id.Equals(id), ct);
        public override async Task<Option<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken ct) => await db.Set<TEntity>().FirstOrDefaultAsync(expression, ct);

        public override bool Any(TId id) => db.Set<TEntity>().Any(i => i.Id == null || i.Id.Equals(id));
        public override bool Any(Expression<Func<TEntity, bool>> condition) => db.Set<TEntity>().Any(condition);

        public override async Task<bool> AnyAsync(TId id, CancellationToken ct) => await db.Set<TEntity>().AnyAsync(i => i.Id == null || i.Id.Equals(id), ct);
        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition, CancellationToken ct) => await db.Set<TEntity>().AnyAsync(condition, ct);


        public override void Add(TEntity item)
        {
            db.Set<TEntity>().Add(item);
        }

        public override async Task AddAsync(TEntity item, CancellationToken ct)
        {
            await db.Set<TEntity>().AddAsync(item, ct);
        }


        protected override bool Edit(Expression<Func<TEntity, bool>> condition,
                                   Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls)
        {
            int effectedRow = db.Set<TEntity>().Where(condition).ExecuteUpdate(setPropertyCalls);
            return effectedRow == 1;
        }

        protected override bool Edit(TId id,
                                    Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls)
        {
            int effectedRow = db.Set<TEntity>().Where(i => i.Id == null || i.Id.Equals(id)).ExecuteUpdate(setPropertyCalls);
            return effectedRow == 1;
        }

        protected override async Task<bool> EditAsync(Expression<Func<TEntity, bool>> condition,
                                                     Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>().Where(condition).ExecuteUpdateAsync(setPropertyCalls, ct);
            return effectedRow == 1;
        }

        protected override async Task<bool> EditAsync(TId id,
                                                     Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>().Where(i => i.Id == null || i.Id.Equals(id)).ExecuteUpdateAsync(setPropertyCalls, ct);
            return effectedRow == 1;
        }


        public override bool Delete(TId id)
        {
            int effectedRow = db.Set<TEntity>().Where(i => i.Id == null || i.Id.Equals(id)).ExecuteDelete();
            return effectedRow == 1;
        }

        public override bool Delete(Expression<Func<TEntity, bool>> condition)
        {
            int effectedRow = db.Set<TEntity>().Where(condition).ExecuteDelete();
            return effectedRow == 1;
        }


        public override async Task<bool> DeleteAsync(TId id, CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>().Where(i => i.Id == null || i.Id.Equals(id)).ExecuteDeleteAsync(ct);
            return effectedRow == 1;
        }

        public override async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>().Where(condition).ExecuteDeleteAsync(ct);
            return effectedRow == 1;
        }
    }
}
