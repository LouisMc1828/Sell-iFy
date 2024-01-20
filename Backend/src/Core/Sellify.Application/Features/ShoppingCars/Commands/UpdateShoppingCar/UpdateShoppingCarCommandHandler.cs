using System.Linq.Expressions;
using AutoMapper;
using MediatR;

using Sellify.Application.Exceptions;
using Sellify.Application.Features.ShoppingCars.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.ShoppingCars.Commands.UpdateShoppingCar;

public class UpdateShoppingCarCommandHandler : IRequestHandler<UpdateShoppingCarCommand, ShoppingCarVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateShoppingCarCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShoppingCarVm> Handle(UpdateShoppingCarCommand request, CancellationToken cancellationToken)
    {
        var shoppingCarUpdate = await _unitOfWork.Repository<ShoppingCar>().GetEntityAsync
        (
            x => x.ShoppingCarMasterId == request.ShoppingCarId
        );

        if ( shoppingCarUpdate is null )
        {
            throw new NotFoundException(nameof(ShoppingCar), request.ShoppingCarId!);
        }
        var shoppingCarItems = await _unitOfWork.Repository<ShoppingCarItem>().GetAsync
        (
            x => x.ShoppingCarMasterId == request.ShoppingCarId
        );

        _unitOfWork.Repository<ShoppingCarItem>().DeleteRange(shoppingCarItems);

        var shoppingCarItemsToAdd = _mapper.Map<List<ShoppingCarItem>>(request.ShoppingCarItems);
        shoppingCarItemsToAdd.ForEach(
            x => { x.ShoppingCarId = shoppingCarUpdate.Id; x.ShoppingCarMasterId = request.ShoppingCarId;});

        _unitOfWork.Repository<ShoppingCarItem>().AddRange(shoppingCarItemsToAdd!);

        var result = await _unitOfWork.Complete();

        if ( result <= 0)
        {
            throw new Exception("Could not add shoppingcar items");
        }

        var includes = new List<Expression<Func<ShoppingCar, object>>>();
        includes.Add(y => y.ShoppingCarItems!.OrderBy(x => x.Producto));
        var shoppingcar = await _unitOfWork.Repository<ShoppingCar>().GetEntityAsync(
            x => x.ShoppingCarMasterId == request.ShoppingCarId, includes, true
        );

        return _mapper.Map<ShoppingCarVm>(shoppingcar);


    }
}