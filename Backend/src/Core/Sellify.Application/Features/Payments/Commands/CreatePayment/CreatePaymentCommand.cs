using MediatR;
using Sellify.Application.Features.Orders.Vms;

namespace Sellify.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommand : IRequest<OrderVm>
{
    public int OrderId { get; set; }
    public Guid? ShoppingCarMasterId { get; set; }
}