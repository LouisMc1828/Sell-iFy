using MediatR;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Domain;

namespace Sellify.Application.Features.Products.Queries.GetProductList;

public class GetProductListQuery : IRequest<IReadOnlyList<ProductVm>>
{

    

}