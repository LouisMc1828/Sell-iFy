using System.Linq.Expressions;

namespace Sellify.Application.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criterio {get;}

    List<Expression<Func<T, object>>>? Includes {get;}

    Expression<Func<T, object>>? OrderBy {get;}

    Expression<Func<T, object>>? OrderByDescending {get;}

    int Take {get;}

    int Skip {get;}

    bool IsPagingEnable {get;}
}