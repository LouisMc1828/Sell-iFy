using MediatR;
using Sellify.Application.Features.Products.Queries.Vms;

namespace Sellify.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<ProductVm>
{
    public int ProductId { get; set; }

    public DeleteProductCommand(int productId)
    {
        ProductId = productId == 0 ? throw new ArgumentException(nameof(productId)) : productId;
    }
}