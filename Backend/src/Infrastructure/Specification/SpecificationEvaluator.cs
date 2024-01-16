using Microsoft.EntityFrameworkCore;
using Sellify.Application.Specification;

namespace Sellify.Infrastructure.Specification;

public class SpecificationEvaluator<T> where T : class
{

    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        if(spec.Criterio != null)
        {
            inputQuery = inputQuery.Where(spec.Criterio);
        }

        if (spec.OrderBy != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            inputQuery = inputQuery.OrderBy(spec.OrderByDescending);
        }

        if (spec.IsPagingEnable)
        {
            inputQuery = inputQuery.Skip(spec.Skip).Take(spec.Take);
        }

        inputQuery = spec.Includes!.Aggregate(inputQuery, (current, include) =>
                    current.Include(include)).AsSplitQuery().AsNoTracking();

        return inputQuery;

    }

}