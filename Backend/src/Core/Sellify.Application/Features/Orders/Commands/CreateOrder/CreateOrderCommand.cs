using MediatR;
using Sellify.Application.Features.Orders.Vms;

namespace Sellify.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<OrderVm>
{
    public Guid? ShoppingCarId { get; set; }
}