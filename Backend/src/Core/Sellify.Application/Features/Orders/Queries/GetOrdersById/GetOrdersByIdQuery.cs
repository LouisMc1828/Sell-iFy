using MediatR;
using Sellify.Application.Features.Orders.Vms;

namespace Sellify.Application.Features.Orders.Queries.GetOrdersById;

public class GetOrdersByIdQuery : IRequest<OrderVm>
{
    public int OrderId { get; set; }

    public GetOrdersByIdQuery(int orderId)
    {
        this.OrderId = orderId == 0 ? throw new ArgumentNullException(nameof(orderId)) : orderId;
    }
}