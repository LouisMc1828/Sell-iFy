using MediatR;
using Sellify.Application.Features.Shared.Queries;
using Sellify.Application.Persistence;
using Sellify.Application.Specification.Users;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.PaginationUsers;

public class PaginationUsersQueryHandler : IRequestHandler<PaginationUsersQuery, PaginationVm<Usuario>>
{

    private readonly IUnitOfWork _unitOfWork;

    public PaginationUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginationVm<Usuario>> Handle(PaginationUsersQuery request, CancellationToken cancellationToken)
    {
        var userSpecificationParams = new UserSpecificationParams
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Search = request.Search,
            Sort = request.Sort
        };

        var spec = new UserSpecification(userSpecificationParams);
        var users = await _unitOfWork.Repository<Usuario>().GetAllWithSpec(spec);

        var specCount = new UserForCountingSpecification(userSpecificationParams);
        var totalUsers = await _unitOfWork.Repository<Usuario>().CountAsync(specCount);

        var rounded = Math.Ceiling(Convert.ToDecimal(totalUsers) / Convert.ToDecimal(request.PageSize));
        var totalPages = Convert.ToInt32(rounded);

        var usersByPage = users.Count();

        var pagination = new PaginationVm<Usuario>
        {
            Count = totalUsers,
            Data = users,
            PageCount = totalPages,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            ResultByPage = usersByPage
        };

        return pagination;
    }
}