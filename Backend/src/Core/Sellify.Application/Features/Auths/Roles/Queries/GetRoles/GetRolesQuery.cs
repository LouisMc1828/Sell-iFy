using MediatR;

namespace Sellify.Application.Features.Auths.Roles.Queries.GetRoles;

public class GetRolesQuery : IRequest<List<string>>
{

}