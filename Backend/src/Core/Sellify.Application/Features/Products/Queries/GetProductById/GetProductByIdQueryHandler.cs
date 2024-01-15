using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductVm>
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;
    

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductVm> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        
        var includes = new List<Expression<Func<Product, object>>>();
        includes.Add(p => p.Images!);
        includes.Add(p => p.Reviews!.OrderByDescending(x => x.CreateDate));

        var product = await _unitOfWork.Repository<Product>().GetEntityAsync(
            x => x.Id == request.ProductId,
            includes,
            true
        );

        return _mapper.Map<ProductVm>(product);



    }
}