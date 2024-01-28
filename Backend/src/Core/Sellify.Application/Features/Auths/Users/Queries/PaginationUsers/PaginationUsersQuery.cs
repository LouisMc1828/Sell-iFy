using MediatR;
using Sellify.Application.Features.Shared.Queries;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Queries.PaginationUsers;

public class PaginationUsersQuery : PaginationBaseQuery, IRequest<PaginationVm<Usuario>>

{
    
}