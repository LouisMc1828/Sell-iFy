using AutoMapper;
using MediatR;
using Sellify.Application.Features.Reviews.Queries.Vms;
using Sellify.Application.Features.Shared.Queries;
using Sellify.Application.Persistence;
using Sellify.Application.Specification.Reviews;
using Sellify.Domain;

namespace Sellify.Application.Features.Reviews.Queries.PaginationReviews;

public class PaginationReviewsQueryHandler : IRequestHandler<PaginationReviewsQuery, PaginationVm<ReviewVm>>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaginationReviewsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationVm<ReviewVm>> Handle(PaginationReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviewSpecParams = new ReviewSpecificationParams{
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort,
            ProductId = request.ProductId
        };

        var spec = new ReviewSpecification(reviewSpecParams);
        var reviews = await _unitOfWork.Repository<Review>().GetAllWithSpec(spec);
        var specCount = new ReviewForCountingSpecification(reviewSpecParams);
        var totalReviews = await _unitOfWork.Repository<Review>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalReviews) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var data = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewVm>>(reviews);

        var reviewByPage = reviews.Count();

        var pagination = new PaginationVm<ReviewVm>{
            Count = totalReviews,
            Data = data,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = reviewByPage
        };
        return pagination;
    }
}