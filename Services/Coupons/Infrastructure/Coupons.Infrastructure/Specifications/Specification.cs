using System.Linq.Expressions;

namespace Coupons.Infrastructure.Specifications;

public abstract class Specification<TEntity>(Expression<Func<TEntity, bool>>? criteria)
{
    public bool IsSplitQuery { get; protected set; }

    public Expression<Func<TEntity, bool>>? Criteria { get; } = criteria;

    public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpression.Add(includeExpression);
}