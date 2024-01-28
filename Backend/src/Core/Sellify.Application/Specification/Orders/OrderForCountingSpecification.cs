using Sellify.Application.Specification;
using Sellify.Domain;

namespace Sellify.Application.Specification.Orders;

public class OrderForCountingSpecification : BaseSpecification<Order>
{
    public OrderForCountingSpecification(OrderSpecificationParams orderParams)
    : base(
        x => (string.IsNullOrEmpty(orderParams.Username) ||
        x.CompradorUsername!.Contains(orderParams.Username))
        && (!orderParams.Id.HasValue || x.Id == orderParams.Id)
    )
    {
    }
}