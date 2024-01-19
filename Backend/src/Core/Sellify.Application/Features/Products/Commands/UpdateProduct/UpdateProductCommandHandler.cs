using AutoMapper;
using MediatR;
using Sellify.Application.Exceptions;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productUpdate = await _unitOfWork.Repository<Product>().GetByIdAsync(request.Id);
        if (productUpdate is null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        _mapper.Map(request, productUpdate, typeof(UpdateProductCommand), typeof(Product));
        await _unitOfWork.Repository<Product>().UpdateAsync(productUpdate);

        if ( (request.ImageUrls is not null) && request.ImageUrls.Count > 0)
        {
            var imageRemove = await _unitOfWork.Repository<Image>().GetAsync(
                x => x.ProductId == request.Id
            );
            _unitOfWork.Repository<Image>().DeleteRange(imageRemove);

            request.ImageUrls.Select(y => {y.ProductId = request.Id; return y;}).ToList();
            var images = _mapper.Map<List<Image>>(request.ImageUrls);
            _unitOfWork.Repository<Image>().AddRange(images);
            await _unitOfWork.Complete();
        }

        return _mapper.Map<ProductVm>(productUpdate);

    }
}