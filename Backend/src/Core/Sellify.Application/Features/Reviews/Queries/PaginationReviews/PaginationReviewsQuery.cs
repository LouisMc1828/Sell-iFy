using MediatR;
using Sellify.Application.Features.Reviews.Queries.Vms;
using Sellify.Application.Features.Shared.Queries;

namespace Sellify.Application.Features.Reviews.Queries.PaginationReviews;

public class PaginationReviewsQuery : PaginationBaseQuery, IRequest<PaginationVm<ReviewVm>>
{
    public int? ProductId { get; set; }
}