

using MediatR;
using Sellify.Application.Features.Categories.Vms;

namespace Sellify.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CategoryVm>
{
    public string? Nombre { get; set; }
}