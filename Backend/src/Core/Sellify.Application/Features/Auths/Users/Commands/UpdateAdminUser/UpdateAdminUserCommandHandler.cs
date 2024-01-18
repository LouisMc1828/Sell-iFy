using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Exceptions;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateAdminUser;

public class UpdateAdminUserCommandHandler : IRequestHandler<UpdateAdminUserCommand, Usuario>
{

    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public UpdateAdminUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    public async Task<Usuario> Handle(UpdateAdminUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.Id!);
        if (updateUser is null)
        {
            throw new BadRequestException("Could not find user");
        }

        updateUser.Nombre = request.Nombre;
        updateUser.Apellido = request.Apellido;
        updateUser.Telefono = request.Telefono;

        var result = await _userManager.UpdateAsync(updateUser);
        if (!result.Succeeded)
        {
            throw new Exception("Could not update user");
        }

        var role = await _roleManager.FindByNameAsync(request.Role!);
        if(role is null)
        {
            throw new Exception("Could not find role");
        }

        await _userManager.AddToRoleAsync(updateUser, role.Name!);

        return updateUser;

    }
}