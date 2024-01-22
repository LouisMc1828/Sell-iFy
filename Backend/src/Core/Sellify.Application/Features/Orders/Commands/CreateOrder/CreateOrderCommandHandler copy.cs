/*using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Application.Models.Payment;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler2 : IRequestHandler<CreateOrderCommand, OrderVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly UserManager<Usuario> _userManager;
    private readonly PayPalSettings _paypalSettings;

    public CreateOrderCommandHandler2(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService, UserManager<Usuario> userManager, IOptions<PayPalSettings> paypalSettings)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
        _userManager = userManager;
        _paypalSettings = paypalSettings.Value;
    }

    public async Task<OrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderPending = await _unitOfWork.Repository<Order>().GetEntityAsync
        (
            x => x.CompradorUsername == _authService.GetSessionUser() && x.Status == OrderStatus.Pending,
            null,
            true
        );
        if (orderPending is not null)
        {
            await _unitOfWork.Repository<Order>().DeleteAsync(orderPending);
        }

        var includes = new List<Expression<Func<ShoppingCar, object>>>();
        includes.Add(x=>x.ShoppingCarItems!.OrderBy(y=>y.Producto));
        var shoppingCar = await _unitOfWork.Repository<ShoppingCar>().GetEntityAsync
        (
            x => x.ShoppingCarMasterId == request.ShoppingCarId,
            includes,
            false
        );

        var user = await _userManager.FindByNameAsync(_authService.GetSessionUser());
        if ( user is null)
        {
            throw new Exception("Could not find user");
        }

        var dir = await _unitOfWork.Repository<Address>().GetEntityAsync
        (
            x => x.Username == user.UserName,
            null,
            false
        );

        OrderAddress orderAd = new OrderAddress (){
            Direccion = dir.Direccion,
            Ciudad = dir.Ciudad,
            CodigoPostal = dir.CodigoPostal,
            Pais = dir.Pais,
            Departamento = dir.Departamento,
            Username = dir.Username
        };

        await _unitOfWork.Repository<OrderAddress>().AddAsync(orderAd);

        var subTotal = Math.Round( shoppingCar.ShoppingCarItems!.Sum( x => x.Precio * x.Cantidad),2);
        var impuesto = Math.Round(subTotal * Convert.ToDecimal(0.18), 2);
        var precioEnvio = subTotal < 1000 ? 100 : 250;
        var total = subTotal + precioEnvio + impuesto;
        var comprador = $"{user.Nombre} {user.Apellido}";
        var order = new Order
        (
            comprador,
            user.UserName!,
            orderAd,
            subTotal,
            total,
            impuesto,
            precioEnvio
        );

        await _unitOfWork.Repository<Order>().AddAsync(order);

        var items = new List<OrderItem>();

        foreach ( var shoppingProduct in shoppingCar.ShoppingCarItems! )
        {
            var orderItem = new OrderItem
            {
                ProductNombre = shoppingProduct.Producto,
                ProductId = shoppingProduct.ProductId,
                ImagenUrl = shoppingProduct.Imagen,
                Precio = shoppingProduct.Precio,
                Cantidad = shoppingProduct.Cantidad,
                OrderId = order.Id
            };
            items.Add( orderItem );
        }

        _unitOfWork.Repository<OrderItem>().AddRange( items );

        var result = await _unitOfWork.Complete();
        if ( result <= 0)
        {
            throw new Exception("Shopping error");
        }

        //seccion de api

        _unitOfWork.Repository<Order>().UpdateEntity(order);

        var resultOrder = await _unitOfWork.Complete();
        if ( resultOrder <= 0 )
        {
            throw new Exception("Payment request failed");
        }

        return _mapper.Map<OrderVm>(order);
    }
}*/

/*
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        var service = new PaymentIntentService();
        PaymentIntent intent;

        if(string.IsNullOrEmpty(order.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)order.Total,
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card"}
            };

            intent = await service.CreateAsync(options);
            order.PaymentIntentId = intent.Id;
            order.ClientSecret = intent.ClientSecret;
            order.StripeApiKey = _stripeSettings.PublishbleKey;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)order.Total
            };
            await service.UpdateAsync(order.PaymentIntentId, options);
        }
}*/