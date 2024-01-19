using AutoMapper;
using MediatR;
using Sellify.Application.Exceptions;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productDelete = await _unitOfWork.Repository<Product>().GetByIdAsync(request.ProductId);
        if ( productDelete is null)
        {
            throw new NotFoundException(nameof(Product), request.ProductId);
        }

        productDelete.Status = productDelete.Status == ProductStatus.Inactivo ? ProductStatus.Activo : ProductStatus.Inactivo;

        await _unitOfWork.Repository<Product>().UpdateAsync(productDelete);

        return _mapper.Map<ProductVm>(productDelete);
    }
}