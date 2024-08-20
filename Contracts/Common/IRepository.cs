using LanguageExt;
using System.Linq.Expressions;

namespace Auth.Contracts.Common
{
    public interface IRepository<TEntity, TId>
    {
        public List<TEntity> ToList();
        public List<TEntity> ToList(Expression<Func<TEntity, bool>> condition);

        public Task<List<TEntity>> ToListAsync(CancellationToken ct);
        public Task<List<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> condition, CancellationToken ct);


        public Option<TEntity> Find(TId id);
        public Option<TEntity> Find(Expression<Func<TEntity, bool>> expression);

        public Task<Option<TEntity>> FindAsync(TId id, CancellationToken ct);
        public Task<Option<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken ct);


        public bool Any(TId id);
        public bool Any(Expression<Func<TEntity, bool>> condition);

        public Task<bool> AnyAsync(TId id, CancellationToken ct);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition, CancellationToken ct);


        public void Add(TEntity item);
        public Task AddAsync(TEntity item, CancellationToken ct);

        public bool Delete(TId id);
        public bool Delete(Expression<Func<TEntity, bool>> condition);

        public Task<bool> DeleteAsync(TId id, CancellationToken ct);
        public Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> condition, CancellationToken ct);
    }
}