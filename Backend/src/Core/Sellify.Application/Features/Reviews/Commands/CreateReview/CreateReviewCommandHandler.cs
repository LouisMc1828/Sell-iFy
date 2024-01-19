using AutoMapper;
using MediatR;
using Sellify.Application.Features.Reviews.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewVm>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ReviewVm> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var reviewEntity = new Review {
            Comentario = request.Comentario,
            Rating = request.Rating,
            Nombre = request.Nombre,
            ProductId = request.ProductId
        };

        _unitOfWork.Repository<Review>().AddEntity(reviewEntity);
        var result = await _unitOfWork.Complete();
        if (result <= 0)
        {
            throw new Exception("Review no complete");
        }
        return _mapper.Map<ReviewVm>(reviewEntity);
    }
}