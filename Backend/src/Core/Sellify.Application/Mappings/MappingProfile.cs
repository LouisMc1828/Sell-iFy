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
        .ForMember(m => m.CategoryNombre, x => x.MapFrom(y => y.Category!.Nombre))
        .ForMember(p => p.NumeroReviews, x => x.MapFrom(y => y.Reviews == null ? 0 : y.Reviews.Count));

        CreateMap<Image, ImageVm>();
        CreateMap<Review, ReviewVm>();
    }
}