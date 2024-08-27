using Auth.Contracts.Response;
using LanguageExt;
using System.Linq.Expressions;

namespace Auth.Contracts.Common
{
    public interface IRepository<TEntity, TId>
    {
        public List<TEntity> ToList();

        public List<TEntity> ToList(Expression<Func<TEntity, bool>> condition);

        public abstract QueryResponse<TResponse> ToList<TResponse>(
                 Func<TEntity, TResponse> selector,
                 Expression<Func<TEntity, bool>>? expression = default,
                 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = default,
                 int pageIndex = 1,
                 int pageSize = 10);

        public Task<List<TEntity>> ToListAsync(
            CancellationToken ct);

        public Task<List<TEntity>> ToListAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct);

        public Task<QueryResponse<TResponse>> ToListAsync<TResponse>(
                  Func<TEntity, TResponse> selector,
                  Expression<Func<TEntity, bool>>? expression = default,
                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = default,
                  int pageIndex = 1,
                  int pageSize = 10,
                  CancellationToken ct = default);

        public Option<TEntity> Find(TId id);

        public Option<TEntity> Find(
            Expression<Func<TEntity, bool>> expression);

        public abstract Option<TResult> Find<TResult>(
                Expression<Func<TEntity, bool>> expression,
                Func<TEntity, TResult> selector);


        public Task<Option<TEntity>> FindAsync(
            TId id, CancellationToken ct);

        public Task<Option<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken ct);

        public abstract Task<Option<TResult>> FindAsync<TResult>(
            Expression<Func<TEntity, bool>> expression,
            Func<TEntity, TResult> selector,
            CancellationToken ct);


        public bool Any(TId id);

        public bool Any(
            Expression<Func<TEntity, bool>> condition);

        public Task<bool> AnyAsync(
            TId id, CancellationToken ct);

        public Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct);


        public void Add(TEntity item);

        public Task AddAsync(
            TEntity item,
            CancellationToken ct);

        public bool Delete(TId id);

        public bool Delete(
            Expression<Func<TEntity, bool>> condition);

        public Task<bool> DeleteAsync(
            TId id, CancellationToken ct);

        public Task<bool> DeleteAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct);
    }
}