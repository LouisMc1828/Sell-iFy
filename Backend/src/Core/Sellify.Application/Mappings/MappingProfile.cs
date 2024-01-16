using AutoMapper;
using Sellify.Application.Features.Images.Queries.Vms;
using Sellify.Application.Features.Products.Queries.Vms;
using Sellify.Application.Features.Reviews.Queries.Vms;
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
    }
}