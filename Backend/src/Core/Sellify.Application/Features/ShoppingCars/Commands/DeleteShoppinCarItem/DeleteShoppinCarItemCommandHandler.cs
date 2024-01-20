using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Sellify.Application.Features.ShoppingCars.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.ShoppingCars.Commands.DeleteShoppinCarItem;

public class DeleteShoppinCarItemCommandHandler : IRequestHandler<DeleteShoppinCarItemCommand, ShoppingCarVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteShoppinCarItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ShoppingCarVm> Handle(DeleteShoppinCarItemCommand request, CancellationToken cancellationToken)
    {
        var shoppingCarItemDelete = await _unitOfWork.Repository<ShoppingCarItem>().GetEntityAsync
        (
            x => x.Id == request.Id
        );

        await _unitOfWork.Repository<ShoppingCarItem>().DeleteAsync(shoppingCarItemDelete);

        var includes = new List<Expression<Func<ShoppingCar, object>>>();
        includes.Add(y => y.ShoppingCarItems!.OrderBy(x => x.Producto));
        var shoppingCar = await _unitOfWork.Repository<ShoppingCar>().GetEntityAsync
        (
            x => x.ShoppingCarMasterId == shoppingCarItemDelete.ShoppingCarMasterId,
            includes,
            true
        );

        return _mapper.Map<ShoppingCarVm>(shoppingCar);
    }
}