using MediatR;
using Sellify.Application.Features.Categories.Vms;

namespace Sellify.Application.Features.Categories.Queries.GetCategoriesList;

public class GetCategoryListQuery : IRequest<IReadOnlyList<CategoryVm>>
{
    
}