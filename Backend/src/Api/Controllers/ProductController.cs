using System.Net;
using Sellify.Application.Contracts.Infrasctructure;
using Sellify.Application.Features.Products.Commands.CreateProduct;
using Sellify.Application.Features.Products.Commands.DeleteProduct;
using Sellify.Application.Features.Products.Commands.UpdateProduct;
using Sellify.Application.Features.Products.Queries.GetProductById;
using Sellify.Application.Features.Products.Queries.GetProductList;
using Sellify.Application.Features.Products.Queries.PaginationProducts;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Features.Shared.Queries;
using Sellify.Application.Models.Authorization;
using Sellify.Application.Models.ImageManagement;
using Sellify.Domain;
using Sellify.Infrastructure.ImageCloudinary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Sellify.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public ProductController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    [AllowAnonymous]
    [HttpGet("list", Name = "GetProductList")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyList<ProductVm>>> GetProductList()
    {
        var query = new GetProductListQuery();
        var products = await _mediator.Send(query);

        return Ok(products);
    }

    [AllowAnonymous]
    [HttpGet("pagination", Name = "PaginationProduct")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProduct(
        [FromQuery] PaginationProductsQuery paginationProductsQuery
    )
    {
        paginationProductsQuery.Status = ProductStatus.Activo;
        var paginationProduct = await _mediator.Send(paginationProductsQuery);
        return Ok(paginationProduct);
    }

    [Authorize(Roles = Role.ADMIN)]
    [HttpGet("paginationAdmin", Name = "PaginationProductAdmin")]
    [ProducesResponseType(typeof(PaginationVm<ProductVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PaginationVm<ProductVm>>> PaginationProductAdmin(
        [FromQuery] PaginationProductsQuery paginationProductsQuery
    )
    {
        var paginationProduct = await _mediator.Send(paginationProductsQuery);
        return Ok(paginationProduct);
    }

    [AllowAnonymous]
    [HttpGet("{id}", Name = "GetProductById")]
    [ProducesResponseType(typeof(ProductVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> GetProductById(int id)
    {
        var query = new GetProductByIdQuery(id);
        return Ok (await _mediator.Send(query));
    }


    [Authorize(Roles = Role.ADMIN)]
    [HttpPost("create", Name = "CreateProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> CreateProduct([FromForm] CreateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();
        if(request.Fotos is not null)
        {
            foreach(var foto in request.Fotos)
            {
                var resultImage = await _manageImageService.UploadImage(
                    new ImageData
                    {
                        ImageStream = foto.OpenReadStream(),
                        Nombre = foto.Name
                    }
                );

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                listFotoUrls.Add(fotoCommand);
            }

            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);
    }



    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("update", Name = "UpdateProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> UpdateProduct([FromForm] UpdateProductCommand request)
    {
        var listFotoUrls = new List<CreateProductImageCommand>();
        if(request.Fotos is not null)
        {
            foreach(var foto in request.Fotos)
            {
                var resultImage = await _manageImageService.UploadImage(
                    new ImageData
                    {
                        ImageStream = foto.OpenReadStream(),
                        Nombre = foto.Name
                    }
                );

                var fotoCommand = new CreateProductImageCommand
                {
                    PublicCode = resultImage.PublicId,
                    Url = resultImage.Url
                };

                listFotoUrls.Add(fotoCommand);
            }

            request.ImageUrls = listFotoUrls;
        }

        return await _mediator.Send(request);
    }


    [Authorize(Roles = Role.ADMIN)]
    [HttpPut("status/{id}", Name = "UpdateStatusProduct")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductVm>> UpdateStatusProduct(int id)
    {
        var request = new DeleteProductCommand(id);
        return await _mediator.Send(request);
    }

}