using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Features.Addresses.Commands.CreateAddress;
using Sellify.Application.Features.Addresses.Vms;
using Sellify.Application.Features.Orders.Commands.CreateOrder;
using Sellify.Application.Features.Orders.Commands.UpdateOrder;
using Sellify.Application.Features.Orders.Queries.GetOrdersById;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Application.Models.Authorization;

namespace Sellify.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private IMediator _mediator;
    private readonly IAuthService _authService;

    public OrderController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }


    [HttpPost("address", Name = "CreateAddress")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AddressVm>> CreateAddress([FromBody] CreateAddressCommand request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost(Name = "CreateOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderVm>> CreateOrder([FromBody] CreateOrderCommand request)
    {
        return await _mediator.Send(request);
    }


    [Authorize (Roles = Role.ADMIN)]
    [HttpPut(Name = "UpdateOrder")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderVm>> UpdateOrder([FromBody] UpdateOrderCommand request)
    {
        return await _mediator.Send(request);
    }


    //[Authorize ( Roles = Role.ADMIN)]
    [HttpGet("{id}", Name = "GetOrderById")]
    [ProducesResponseType(typeof(OrderVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderVm>> GetOrderById(int id)
    {
        var query = new GetOrdersByIdQuery(id);
        return Ok(await _mediator.Send(query));
    }
}