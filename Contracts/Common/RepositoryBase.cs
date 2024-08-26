using System.Linq.Expressions;
using LanguageExt;
using Microsoft.EntityFrameworkCore.Query;

namespace Auth.Contracts.Common
{
    public abstract class RepositoryBase<TEntity, TId>
        : IRepository<TEntity, TId>
    {
        public abstract List<TEntity> ToList();

        public abstract List<TEntity> ToList(
            Expression<Func<TEntity, bool>> condition);

        public abstract Task<List<TEntity>> ToListAsync(
            CancellationToken ct);

        public abstract Task<List<TEntity>> ToListAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct);


        public abstract Option<TEntity> Find(TId id);

        public abstract Option<TEntity> Find(
            Expression<Func<TEntity, bool>> expression);

        public abstract Option<TResult> Find<TResult>(
            Expression<Func<TEntity, bool>> expression,
            Func<TEntity, TResult> selector);


        public abstract Task<Option<TEntity>> FindAsync(
            TId id,
            CancellationToken ct);

        public abstract Task<Option<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken ct);

        public abstract Task<Option<TResult>> FindAsync<TResult>(
            Expression<Func<TEntity, bool>> expression,
            Func<TEntity, TResult> selector,
            CancellationToken ct);



        public abstract bool Any(TId id);

        public abstract bool Any(
            Expression<Func<TEntity, bool>> condition);

        public abstract Task<bool> AnyAsync(
            TId id,
            CancellationToken ct);

        public abstract Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct);


        public abstract void Add(TEntity item);
        public abstract Task AddAsync(
            TEntity item,
            CancellationToken ct);

        public abstract bool Delete(TId id);

        public abstract bool Delete(
            Expression<Func<TEntity, bool>> condition);

        public abstract Task<bool> DeleteAsync(
            TId id,
            CancellationToken ct);

        public abstract Task<bool> DeleteAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct);

        protected abstract bool Edit(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<SetPropertyCalls<TEntity>,
                SetPropertyCalls<TEntity>>> setPropertyCalls);

        protected abstract bool Edit(
            TId id,
            Expression<Func<SetPropertyCalls<TEntity>,
                SetPropertyCalls<TEntity>>> setPropertyCalls);

        protected abstract Task<bool> EditAsync(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<SetPropertyCalls<TEntity>,
                SetPropertyCalls<TEntity>>> setPropertyCalls,
            CancellationToken ct);

        protected abstract Task<bool> EditAsync(
            TId id,
            Expression<Func<SetPropertyCalls<TEntity>,
                SetPropertyCalls<TEntity>>> setPropertyCalls,
            CancellationToken ct);
    }
}