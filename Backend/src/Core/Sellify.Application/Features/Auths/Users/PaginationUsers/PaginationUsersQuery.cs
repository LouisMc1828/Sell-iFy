using MediatR;
using Sellify.Application.Features.Shared.Queries;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.PaginationUsers;

public class PaginationUsersQuery : PaginationBaseQuery, IRequest<PaginationVm<Usuario>>

{
    
}