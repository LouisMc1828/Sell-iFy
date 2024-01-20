using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.ShoppingCars.Commands.DeleteShoppinCarItem;
using Sellify.Application.Features.ShoppingCars.Commands.UpdateShoppingCar;
using Sellify.Application.Features.ShoppingCars.Queries.GetShoppingCarById;
using Sellify.Application.Features.ShoppingCars.Vms;

namespace Sellify.Api.Controllers;




[ApiController]
[Route("/api/v1/[controller]")]
public class ShoppingCarController : ControllerBase
{
    private IMediator _mediator;

    public ShoppingCarController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetShoppingCar")]
    [ProducesResponseType(typeof(ShoppingCarVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCarVm>> GetShoppingCar(Guid id)
    {
        var shoppingCar = id == Guid.Empty ? Guid.NewGuid() : id;
        var query = new GetShoppingCarByIdQuery(shoppingCar);

        return await _mediator.Send(query);
    }

    [AllowAnonymous]
    [HttpPut("{id}", Name = "UpdateShoppingCar")]
    [ProducesResponseType(typeof(ShoppingCarVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCarVm>> UpdateShoppingCar(Guid id, UpdateShoppingCarCommand request)
    {

        request.ShoppingCarId = id;
        return await _mediator.Send(request);
    }

    [AllowAnonymous]
    [HttpDelete("item/{id}", Name = "DeleteShoppingCar")]
    [ProducesResponseType(typeof(ShoppingCarVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCarVm>> DeleteShoppingCar(int id)
    {

        return await _mediator.Send(new DeleteShoppinCarItemCommand() { Id=id });
    }
}