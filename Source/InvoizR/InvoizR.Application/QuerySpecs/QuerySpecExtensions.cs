using InvoizR.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace InvoizR.Application.QuerySpecs;

public static class QuerySpecExtensions
{
    public static IQueryable<TQueryModel> Spec<TQueryModel>(this IQueryable<TQueryModel> query, ISpecification<TQueryModel> spec) where TQueryModel : class
    {
        // fetch a Queryable that includes all expression-based includes
        var queryableResultWithIncludes = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        // modify the IQueryable to include any string-based include statements
        var secondaryResult = spec.IncludeStrings.Aggregate(queryableResultWithIncludes, (current, include) => current.Include(include));

        // return the result of the query using the specification's criteria expression
        return spec.Criteria == null ? secondaryResult : secondaryResult.Where(spec.Criteria);
    }
}
