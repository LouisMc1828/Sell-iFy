using Sellify.Domain;

namespace Sellify.Application.Specification.Orders;

public class OrderSpecification : BaseSpecification<Order>
{
    public OrderSpecification(OrderSpecificationParams orderParams) : base(
        x => (string.IsNullOrEmpty(orderParams.Username) ||
        x.CompradorUsername!.Contains(orderParams.Username))
        && (!orderParams.Id.HasValue || x.Id == orderParams.Id)
    )
    {
        AddInclude(y => y.OrderItems!);

        ApplyPaging(orderParams.PageSize * (orderParams.PageIndex - 1), orderParams.PageSize);

        if ( !string.IsNullOrEmpty(orderParams.Sort))
        {
            switch (orderParams.Sort)
            {
                case "createDateAsc":
                    AddOrderBy(z => z.CreatedDate!);
                break;

                case "createDateDesc":
                    AddOrderByDescending(z => z.CreatedDate!);
                break;

                default:
                    AddOrderBy(z => z.CreatedDate!);
                break;
            }
        }
        else
        {
            AddOrderByDescending(z => z.CreatedDate!);
        }

    }
}