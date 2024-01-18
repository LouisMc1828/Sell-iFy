using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Exceptions;
using Sellify.Application.Features.Auths.Users.Vms;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, AuthResponse>
{

    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public UpdateUserCommandHandler
    (
        UserManager<Usuario> userManager,
        RoleManager<IdentityRole> roleManager,
        IAuthService authService
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());
        if (updateUser is null)
        {
            throw new BadRequestException("Could not find");
        }

        updateUser.Nombre = request.Nombre ?? updateUser.Nombre;
        updateUser.Apellido = request.Apellido ?? updateUser.Apellido;
        updateUser.Telefono = request.Telefono ?? updateUser.Telefono;
        updateUser.AvatarUrl = request.FotoUrl ?? updateUser.AvatarUrl;

        var result = await _userManager.UpdateAsync(updateUser);
        if (!result.Succeeded)
        {
            throw new BadRequestException("Could not update");
        }

        var userById = await _userManager.FindByEmailAsync(request.Email!);
        var roles = await _userManager.GetRolesAsync(userById!);

        return new AuthResponse
        {
            Id = userById!.Id,
            Nombre = userById.Nombre,
            Apellido = userById.Apellido,
            Telefono = userById.Telefono,
            Email = userById.Email,
            Username = userById.UserName,
            Avatar = userById.AvatarUrl,
            Token = _authService.CreateToken(userById, roles),
            Roles = roles
        };
    }
}