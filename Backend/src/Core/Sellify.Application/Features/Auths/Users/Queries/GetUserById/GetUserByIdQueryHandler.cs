using MediatR;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Engines;
using Sellify.Application.Features.Auths.Users.Vms;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, AuthResponse>
{

    private readonly UserManager<Usuario> _userManager;

    public GetUserByIdQueryHandler(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task<AuthResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId!);
        if (user is null)
        {
            throw new Exception("Could not find user");
        }

        return new AuthResponse
        {
            Id = user.Id,
            Nombre = user.Nombre,
            Apellido = user.Apellido,
            Telefono = user.Telefono,
            Email = user.Email,
            Username = user.UserName,
            Avatar = user.AvatarUrl,
            Roles = await _userManager.GetRolesAsync(user)
        };
    }
}