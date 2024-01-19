using Sellify.Domain;

namespace Sellify.Application.Specification.Reviews;

public class ReviewForCountingSpecification : BaseSpecification<Review>
{
    public ReviewForCountingSpecification(ReviewSpecificationParams reviewParams) : base
    (
        x => (!reviewParams.ProductId.HasValue || x.ProductId == reviewParams.ProductId)
    )
    {
        
    }
}