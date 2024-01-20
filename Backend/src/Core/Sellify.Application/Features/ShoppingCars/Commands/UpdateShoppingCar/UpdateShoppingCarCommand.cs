using MediatR;
using Sellify.Application.Features.ShoppingCars.Vms;


namespace Sellify.Application.Features.ShoppingCars.Commands.UpdateShoppingCar;

public class UpdateShoppingCarCommand : IRequest<ShoppingCarVm>
{
    public Guid? ShoppingCarId { get; set; }

    public List<ShoppingCarItemVm>? ShoppingCarItems { get; set; }
}