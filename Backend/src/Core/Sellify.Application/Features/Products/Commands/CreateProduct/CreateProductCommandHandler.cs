using AutoMapper;
using MediatR;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<Product>(request);
        await _unitOfWork.Repository<Product>().AddAsync(productEntity);
        if ( (request.ImageUrls is not null) && request.ImageUrls.Count > 0 )
        {
            request.ImageUrls.Select( x => {x.ProductId = productEntity.Id; return x;}).ToList();

            var images = _mapper.Map<List<Image>>(request.ImageUrls);
            _unitOfWork.Repository<Image>().AddRange(images);
            await _unitOfWork.Complete();
        }

        return _mapper.Map<ProductVm>(productEntity);
    }
}