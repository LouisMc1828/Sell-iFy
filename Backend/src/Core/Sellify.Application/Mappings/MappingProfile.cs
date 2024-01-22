using AutoMapper;
using Sellify.Application.Features.Addresses.Vms;
using Sellify.Application.Features.Categories.Vms;
using Sellify.Application.Features.Countries.Vms;
using Sellify.Application.Features.Images.Queries.Vms;
using Sellify.Application.Features.Orders.Vms;
using Sellify.Application.Features.Products.Commands.CreateProduct;
using Sellify.Application.Features.Products.Commands.UpdateProduct;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Features.Reviews.Queries.Vms;
using Sellify.Application.Features.ShoppingCars.Vms;
using Sellify.Domain;

namespace Sellify.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductVm>()
        .ForMember(p=> p.CategoryNombre, x=> x.MapFrom(a => a.Category!.Nombre))
        .ForMember(p => p.NumeroReviews, x => x.MapFrom(a => a.Reviews == null ? 0 : a.Reviews.Count));

        CreateMap<Image, ImageVm>();
        CreateMap<Review, ReviewVm>();
        CreateMap<Country, CountryVm>();
        CreateMap<Category, CategoryVm>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
        CreateMap<CreateProductImageCommand, Image>();
        CreateMap<ShoppingCar, ShoppingCarVm>()
            .ForMember(x => x.ShoppingCarId, y => y.MapFrom(z => z.ShoppingCarMasterId));
        CreateMap<ShoppingCarItem, ShoppingCarItemVm>();
        CreateMap<ShoppingCarItemVm, ShoppingCarItem>();
        CreateMap<Address, AddressVm>();
        CreateMap<Order, OrderVm>();
        CreateMap<OrderItem, OrderItemVm>();
        CreateMap<OrderAddress, AddressVm>();

    }
}