using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Application.Features.Payments.Commands.CreatePayment;

namespace Sellify.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class PaymentController : ControllerBase
{
    private IMediator _mediator;
    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }



    [HttpPost(Name = "CreatePayment")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderVm>> CreatePayment ([FromBody] CreatePaymentCommand request)
    {
        return await _mediator.Send(request);
    }
}