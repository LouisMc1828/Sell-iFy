using MediatR;
using Sellify.Application.Features.Reviews.Queries.Vms;

namespace Sellify.Application.Features.Reviews.Commands.DeleteReview;

public class DeleteReviewCommand : IRequest
{
    public int ReviewId { get; set; }

    public DeleteReviewCommand(int reviewId)
    {
        ReviewId = reviewId == 0 ? throw new ArgumentException(nameof(reviewId)) : reviewId;
    }


}