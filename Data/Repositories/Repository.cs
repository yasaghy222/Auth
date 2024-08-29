using LanguageExt;
using System.Linq.Expressions;
using Auth.Contracts.Common;
using Auth.Contracts.Response;
using Microsoft.EntityFrameworkCore;
using Auth.Domain.Aggregates.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Auth.Data.Repositories
{
    public class Repository<TEntity, TId>(AuthDBContext db)
        : RepositoryBase<TEntity, TId> where TEntity : class,
        IIdentityAggregate<TId>
    {
        public override List<TEntity> ToList()
        {
            return [.. db.Set<TEntity>().AsNoTracking()];
        }

        public override List<TEntity> ToList(
            Expression<Func<TEntity, bool>> condition)
        {
            return [.. db.Set<TEntity>().Where(condition).AsNoTracking()];
        }

        public override QueryResponse<TResponse> ToList<TResponse>(
           Func<TEntity, TResponse> selector,
           Expression<Func<TEntity, bool>>? expression = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
           int pageIndex = 1,
           int pageSize = 10)
        {
            IQueryable<TEntity> baseQuery = db.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

            // Apply filtering
            if (expression != null)
            {
                baseQuery = baseQuery.Where(expression);
            }

            // Calculate total count before applying pagination
            int totalCount = baseQuery.Count();

            // Apply ordering if any
            IQueryable<TEntity> itemsQuery = order != null ? order(baseQuery) : baseQuery;

            // Apply pagination
            itemsQuery = itemsQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            // Execute the query and project the results
            List<TResponse> items = itemsQuery.Select(selector).ToList();

            return new QueryResponse<TResponse>
            {
                Items = items,
                Count = items.Count,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPageIndex = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public override async Task<List<TEntity>> ToListAsync(
            CancellationToken ct)
        {
            return await db.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public override async Task<List<TEntity>> ToListAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct)
        {
            return await db.Set<TEntity>()
                .Where(condition)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public override async Task<QueryResponse<TResponse>> ToListAsync<TResponse>(
            Func<TEntity, TResponse> selector,
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? order = null,
            int pageIndex = 1,
            int pageSize = 10,
            CancellationToken ct = default)
        {
            IQueryable<TEntity> baseQuery = db.Set<TEntity>()
                .AsNoTracking()
                .AsQueryable();

            // Apply filtering
            if (expression != null)
            {
                baseQuery = baseQuery.Where(expression);
            }

            // Calculate total count before applying pagination
            int totalCount = await baseQuery.CountAsync(ct);

            // Apply ordering if any
            IQueryable<TEntity> itemsQuery = order != null ? order(baseQuery) : baseQuery;

            // Apply pagination
            itemsQuery = itemsQuery.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            // Execute the query and project the results
            List<TResponse> items = await Task.Run(itemsQuery.Select(selector).ToList, ct);

            return new QueryResponse<TResponse>
            {
                Items = items,
                Count = items.Count,
                TotalCount = totalCount,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalPageIndex = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }



        public override Option<TEntity> Find(TId id)
        {
            return db.Set<TEntity>()
                .FirstOrDefault(i => i.Id == null || i.Id.Equals(id));
        }

        public override Option<TEntity> Find(
            Expression<Func<TEntity, bool>> expression)
        {
            return db.Set<TEntity>().FirstOrDefault(expression);
        }

        public override Option<TResult> Find<TResult>(
          Expression<Func<TEntity, bool>> expression,
          Func<TEntity, TResult> selector)
        {
            IQueryable<TResult> query = db.Set<TEntity>()
                .Where(expression).Select(selector).AsQueryable();

            TResult? result = query.FirstOrDefault();

            return result == null ? Option<TResult>.None
                : Option<TResult>.Some(result);
        }


        public override async Task<Option<TEntity>> FindAsync(
            TId id, CancellationToken ct)
        {
            return await db.Set<TEntity>()
                .FirstOrDefaultAsync(i => i.Id == null || i.Id.Equals(id), ct);
        }


        public override async Task<Option<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> expression,
            CancellationToken ct)
        {
            return await db.Set<TEntity>()
                .FirstOrDefaultAsync(expression, ct);
        }

        public override async Task<Option<TResult>> FindAsync<TResult>(
            Expression<Func<TEntity, bool>> expression,
            Func<TEntity, TResult> selector,
            CancellationToken ct)
        {
            IQueryable<TResult> query = db.Set<TEntity>()
                .Where(expression).Select(selector).AsQueryable();

            TResult? result = await Task.FromResult(query.FirstOrDefault());

            return result == null ? Option<TResult>.None
                : Option<TResult>.Some(result);
        }

        public override bool Any(TId id)
        {
            return db.Set<TEntity>()
                .Any(i => i.Id == null || i.Id.Equals(id));
        }

        public override bool Any(
            Expression<Func<TEntity, bool>> condition)
        {
            return db.Set<TEntity>().Any(condition);
        }

        public override async Task<bool> AnyAsync(
            TId id, CancellationToken ct)
        {
            return await db.Set<TEntity>()
                .AnyAsync(i => i.Id == null || i.Id.Equals(id), ct);
        }

        public override async Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct)
        {
            return await db.Set<TEntity>()
                .AnyAsync(condition, ct);
        }

        public override void Add(TEntity item)
        {
            db.Set<TEntity>().Add(item);
        }

        public override async Task AddAsync(
            TEntity item, CancellationToken ct)
        {
            await db.Set<TEntity>().AddAsync(item, ct);
        }

        protected override bool Edit(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<SetPropertyCalls<TEntity>,
            SetPropertyCalls<TEntity>>> setPropertyCalls)
        {
            int effectedRow = db.Set<TEntity>()
                .Where(condition)
                .ExecuteUpdate(setPropertyCalls);

            return effectedRow == 1;
        }

        protected override bool Edit(
            TId id,
            Expression<Func<SetPropertyCalls<TEntity>,
            SetPropertyCalls<TEntity>>> setPropertyCalls)
        {
            int effectedRow = db.Set<TEntity>()
                .Where(i => i.Id == null || i.Id.Equals(id))
                .ExecuteUpdate(setPropertyCalls);

            return effectedRow == 1;
        }

        protected override async Task<bool> EditAsync(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<SetPropertyCalls<TEntity>,
            SetPropertyCalls<TEntity>>> setPropertyCalls,
            CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>()
                .Where(condition)
                .ExecuteUpdateAsync(setPropertyCalls, ct);

            return effectedRow == 1;
        }

        protected override async Task<bool> EditAsync(
            TId id,
            Expression<Func<SetPropertyCalls<TEntity>,
            SetPropertyCalls<TEntity>>> setPropertyCalls,
            CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>()
                .Where(i => i.Id == null || i.Id.Equals(id))
                .ExecuteUpdateAsync(setPropertyCalls, ct);

            return effectedRow == 1;
        }


        public override bool Delete(TId id)
        {
            int effectedRow = db.Set<TEntity>()
            .Where(i => i.Id == null || i.Id.Equals(id))
            .ExecuteDelete();

            return effectedRow == 1;
        }

        public override bool Delete(
            Expression<Func<TEntity, bool>> condition)
        {
            int effectedRow = db.Set<TEntity>()
                .Where(condition)
                .ExecuteDelete();

            return effectedRow == 1;
        }


        public override async Task<bool> DeleteAsync(
            TId id, CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>()
                .Where(i => i.Id == null || i.Id.Equals(id))
                .ExecuteDeleteAsync(ct);

            return effectedRow == 1;
        }

        public override async Task<bool> DeleteAsync(
            Expression<Func<TEntity, bool>> condition,
            CancellationToken ct)
        {
            int effectedRow = await db.Set<TEntity>()
                .Where(condition)
                .ExecuteDeleteAsync(ct);

            return effectedRow == 1;
        }
    }
}
