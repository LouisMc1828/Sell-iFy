
using MediatR;
using Sellify.Application.Features.Categories.Vms;
using Sellify.Application.Persistence;

namespace Sellify.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = _mapper;
    }

    public async Task<CategoryVm> Handle(CreateCategoryCommand request, CancellationToken)
    {
        
    }
}