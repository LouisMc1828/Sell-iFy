using MediatR;
using Sellify.Application.Features.ShoppingCars.Vms;

namespace Sellify.Application.Features.ShoppingCars.Commands.DeleteShoppinCarItem;

public class DeleteShoppinCarItemCommand : IRequest<ShoppingCarVm>
{
    public int Id { get; set; }
}