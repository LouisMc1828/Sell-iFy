using MediatR;
using Sellify.Application.Features.ShoppingCars.Vms;

namespace Sellify.Application.Features.ShoppingCars.Queries.GetShoppingCarById;

public class GetShoppingCarByIdQuery : IRequest<ShoppingCarVm>
{
    public Guid? ShoppingCarId { get; set; }

    public GetShoppingCarByIdQuery(Guid? shoppingCarId)
    {
        ShoppingCarId = shoppingCarId ?? throw new ArgumentNullException(nameof(shoppingCarId));
    }
}