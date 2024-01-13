using System.Linq.Expressions;

namespace Sellify.Application.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification()
    {

    }

    public BaseSpecification(Expression<Func<T, bool>> criterio)
    {
        Criterio = criterio;
    }

    public Expression<Func<T, bool>>? Criterio { get; }

    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, Object>>>();

    public Expression<Func<T, object>>? OrderBy { get; private set;}

    public Expression<Func<T, object>>? OrderByDescending { get; private set;}

    public int Take { get; private set;}

    public int Skip { get; private set;}

    public bool IsPagingEnable { get; private set;}


    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected void ApplyPaging( int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnable = true;
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}