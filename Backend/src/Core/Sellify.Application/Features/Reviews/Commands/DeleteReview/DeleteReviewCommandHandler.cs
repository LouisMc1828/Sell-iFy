using MediatR;
using Sellify.Application.Exceptions;
using Sellify.Application.Features.Reviews.Queries.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var reviewDelete = await _unitOfWork.Repository<Review>().GetByIdAsync(request.ReviewId);
        if (reviewDelete is null)
        {
            throw new NotFoundException(nameof(Review), request.ReviewId);
        }

        _unitOfWork.Repository<Review>().DeleteEntity(reviewDelete);
        await _unitOfWork.Complete();

        return Unit.Value;
    }
}