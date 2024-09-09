using System.Linq.Expressions;

namespace Auth.Shared.Extensions
{
    public class ReplacingExpressionVisitor : ExpressionVisitor
    {
        private readonly Dictionary<Expression, Expression> replacements = [];

        public void Add(Expression from, Expression to) =>
            replacements[from] = to;

        public override Expression? Visit(Expression? node) =>
            node != null && replacements
                .TryGetValue(node, out var replacement)
                ? replacement
                : base.Visit(node);
    }

    public static class ExpressionExtensions
    {
        private static ReplacingExpressionVisitor GetVisitor<T>(
            ParameterExpression parameter,
            Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            // Create a new visitor and add replacements manually
            ReplacingExpressionVisitor visitor = new();
            visitor.Add(expr1.Parameters[0], parameter);
            visitor.Add(expr2.Parameters[0], parameter);

            return visitor;
        }

        // Utility method to combine expressions with AND
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameter =
                Expression.Parameter(typeof(T));

            ReplacingExpressionVisitor visitor =
                GetVisitor(parameter, expr1, expr2);

            // Visit the body and combine expressions with AndAlso
            Expression combined = Expression.AndAlso(
                visitor.Visit(expr1.Body)!,
                visitor.Visit(expr2.Body)!
            );

            return Expression.Lambda<Func<T, bool>>
                (combined, parameter);
        }

        // Utility method to combine expressions with OR
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            ParameterExpression parameter =
                Expression.Parameter(typeof(T));

            // Create a new visitor and add replacements manually
            ReplacingExpressionVisitor visitor =
                GetVisitor(parameter, expr1, expr2);

            // Visit the body and combine expressions with OrElse
            Expression combined = Expression.OrElse(
                visitor.Visit(expr1.Body)!,
                visitor.Visit(expr2.Body)!
            );

            return Expression.Lambda<Func<T, bool>>
                (combined, parameter);
        }
    }
}