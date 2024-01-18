using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Features.Categories.Queries.GetCategoriesList;
using Sellify.Application.Features.Categories.Vms;

namespace Sellify.Api.Controllers;



[ApiController]
[Route("/api/v1/[controller]")]
public class CategoryController : ControllerBase
{

    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpGet("list", Name ="GetCategoriesList")]
    [ProducesResponseType(typeof(IReadOnlyList<CategoryVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<CategoryVm>>> GetCategoriesList()
    {
        var query = new GetCategoryListQuery();
        return Ok(await _mediator.Send(query));
    }

}