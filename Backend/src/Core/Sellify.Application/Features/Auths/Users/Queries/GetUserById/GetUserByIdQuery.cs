using MediatR;
using Sellify.Application.Features.Auths.Users.Vms;

namespace Sellify.Application.Features.Auths.Users.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<AuthResponse>
{
    public string? UserId { get; set; }

    public GetUserByIdQuery(string userId)
    {
        UserId = userId ?? throw new ArgumentNullException(nameof(UserId));
    }
}