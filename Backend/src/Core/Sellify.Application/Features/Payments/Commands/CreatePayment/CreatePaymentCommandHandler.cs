using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Application.Models.Payment;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Payments.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, OrderVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OrderVm> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var orderToPay = await _unitOfWork.Repository<Order>().GetEntityAsync(
            x => x.Id == request.OrderId,
            null,
            false
        );

        orderToPay.Status = OrderStatus.Completed;
        _unitOfWork.Repository<Order>().UpdateEntity(orderToPay);

        var shoppingCarItems = await _unitOfWork.Repository<ShoppingCarItem>().GetAsync(
            x => x.ShoppingCarMasterId == request.ShoppingCarMasterId
        );
        _unitOfWork.Repository<ShoppingCarItem>().DeleteRange(shoppingCarItems);
        await _unitOfWork.Complete();

        return _mapper.Map<OrderVm>(orderToPay);
    }
}