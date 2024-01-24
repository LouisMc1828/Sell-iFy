using MediatR;
using Sellify.Application.Features.ShoppingCars.Vms;

namespace Sellify.Application.Features.ShoppingCars.Commands.DeleteShoppingCarItem;

public class DeleteShoppingCarItemCommand : IRequest<ShoppingCarVm>
{
    public int Id { get; set; }
}