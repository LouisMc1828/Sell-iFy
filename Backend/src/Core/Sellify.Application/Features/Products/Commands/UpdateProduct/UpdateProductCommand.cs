using MediatR;
using Microsoft.AspNetCore.Http;
using Sellify.Application.Features.Products.Commands.CreateProduct;
using Sellify.Application.Features.Products.Queries.Vms;

namespace Sellify.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<ProductVm>
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public string? Vendedor { get; set; }

    public string? CategoryId { get; set; }

    public int Stock { get; set; }

    public IReadOnlyList<IFormFile>? Fotos { get; set; }

    public IReadOnlyList<CreateProductImageCommand>? ImageUrls { get; set; }
}