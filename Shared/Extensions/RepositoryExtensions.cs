using Auth.Contracts.Enums;
using System.Linq.Expressions;
using Auth.Contracts.Common;
using Auth.Domain.Aggregates.Interfaces;

namespace Auth.Shared.Extensions
{
    public static class RepositoryExtensions
    {
        public static Expression<Func<TEntity, bool>>? GetDateExpression<TEntity>(
            this DateFilterDto request)
            where TEntity : IAuditedAggregate
        {
            Expression<Func<TEntity, bool>>? dateExpression = null;

            if (request.FromCreateAt.HasValue && !request.ToCreateAt.HasValue)
            {
                Expression<Func<TEntity, bool>>? fromDateExpression = request.FromCreateAtComparisonType switch
                {
                    QueryComparisonType.EqualTo => u => u.CreateAt == request.FromCreateAt,
                    QueryComparisonType.NotEqualTo => u => u.CreateAt != request.FromCreateAt,
                    QueryComparisonType.GreaterThanOrEqualTo => u => u.CreateAt >= request.FromCreateAt,
                    QueryComparisonType.GreaterThan => (Expression<Func<TEntity, bool>>)(u => u.CreateAt > request.FromCreateAt),
                    _ => null
                };

                if (fromDateExpression != null)
                {
                    dateExpression = dateExpression == null ? fromDateExpression : dateExpression.AndAlso(fromDateExpression);
                }
            }

            if (request.ToCreateAt.HasValue && !request.FromCreateAt.HasValue)
            {
                Expression<Func<TEntity, bool>>? toDateExpression = request.ToCreateAtComparisonType switch
                {
                    QueryComparisonType.EqualTo => u => u.CreateAt == request.ToCreateAt,
                    QueryComparisonType.NotEqualTo => u => u.CreateAt != request.ToCreateAt,
                    QueryComparisonType.LessThanOrEqualTo => u => u.CreateAt <= request.ToCreateAt,
                    QueryComparisonType.LessThan => (Expression<Func<TEntity, bool>>)(u => u.CreateAt < request.ToCreateAt),
                    _ => null
                };

                if (toDateExpression != null)
                {
                    dateExpression = dateExpression == null ? toDateExpression : dateExpression.AndAlso(toDateExpression);
                }
            }

            if (request.FromCreateAt.HasValue && request.ToCreateAt.HasValue)
            {
                // Both FromCreateAt and ToCreateAt are set
                Expression<Func<TEntity, bool>> fromExpression = request.FromCreateAtComparisonType switch
                {
                    QueryComparisonType.GreaterThanOrEqualTo => entity => entity.CreateAt >= request.FromCreateAt,
                    QueryComparisonType.GreaterThan => entity => entity.CreateAt > request.FromCreateAt,
                    _ => entity => true // Default case, should normally not happen
                };

                Expression<Func<TEntity, bool>> toExpression = request.ToCreateAtComparisonType switch
                {
                    QueryComparisonType.LessThanOrEqualTo => entity => entity.CreateAt <= request.ToCreateAt,
                    QueryComparisonType.LessThan => entity => entity.CreateAt < request.ToCreateAt,
                    _ => entity => true // Default case, should normally not happen
                };

                dateExpression = fromExpression.AndAlso(toExpression);
            }

            return dateExpression;
        }
    }
}