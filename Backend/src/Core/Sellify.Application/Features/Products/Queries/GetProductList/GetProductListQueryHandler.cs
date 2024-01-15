using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductVm>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductVm>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Product, object>>>();
        includes.Add(p => p.Images!);
        includes.Add(p => p.Reviews!);

        var products = await _unitOfWork.Repository<Product>().GetAsync(
            null,
            x=>x.OrderBy(y=>y.Nombre),
            includes,
            true
        );

        var productsVm = _mapper.Map<IReadOnlyList<ProductVm>>(products);

        return productsVm;
    }
}