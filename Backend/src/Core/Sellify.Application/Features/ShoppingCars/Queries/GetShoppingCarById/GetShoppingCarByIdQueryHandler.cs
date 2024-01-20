using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Sellify.Application.Features.ShoppingCars.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.ShoppingCars.Queries.GetShoppingCarById;

public class GetShoppingCarByIdQueryHandler : IRequestHandler<GetShoppingCarByIdQuery, ShoppingCarVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetShoppingCarByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShoppingCarVm> Handle(GetShoppingCarByIdQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<ShoppingCar, object>>>();
        includes.Add(x => x.ShoppingCarItems!.OrderBy(p => p.Producto));

        var shoppingCar = await _unitOfWork.Repository<ShoppingCar>().GetEntityAsync
        (
            x => x.ShoppingCarMasterId == request.ShoppingCarId, includes, true
        );

        if ( shoppingCar is null)
        {
            shoppingCar = new ShoppingCar
            {
                ShoppingCarMasterId = request.ShoppingCarId,
                ShoppingCarItems = new List<ShoppingCarItem>()
            };
            _unitOfWork.Repository<ShoppingCar>().AddEntity(shoppingCar);
            await _unitOfWork.Complete();
        }

        return _mapper.Map<ShoppingCarVm>(shoppingCar);
    }
}