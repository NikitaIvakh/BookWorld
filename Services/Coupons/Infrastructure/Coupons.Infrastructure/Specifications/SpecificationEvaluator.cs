using Coupons.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Coupons.Infrastructure.Specifications;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> query, Specification<TEntity> specification)
        where TEntity : Entity
    {
        var inputQuery = query;

        if (specification.Criteria is not null)
            inputQuery = inputQuery.Where(specification.Criteria);

        specification.IncludeExpression.Aggregate(inputQuery, (current, includeExpression) => current.Include(includeExpression));

        if (specification.IsSplitQuery)
            inputQuery = inputQuery.AsSingleQuery();

        return inputQuery;
    }
}