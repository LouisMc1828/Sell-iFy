using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Products.Queries.GetProductList;
using Sellify.Domain;
using System.Net;

namespace Sellify.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProducController : ControllerBase
{
    private IMediator _mediator;

    public ProducController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductList()
    {
        var query = new GetProductListQuery();
        var products = await _mediator.Send(query);

        return Ok(products);
    }

}
