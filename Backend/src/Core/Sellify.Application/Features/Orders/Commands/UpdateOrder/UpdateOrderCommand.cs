using MediatR;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Domain;

namespace Sellify.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<OrderVm>
{
    public int OrderId { get; set; }

    public OrderStatus Status { get; set; }
}