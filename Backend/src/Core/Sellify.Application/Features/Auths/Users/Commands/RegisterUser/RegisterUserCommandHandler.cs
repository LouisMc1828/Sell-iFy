using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Exceptions;
using Sellify.Application.Features.Auths.Users.Vms;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{

    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IAuthService _authService;

    public RegisterUserCommandHandler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager, IAuthService authService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService;
    }

    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existUserByEmail = await _userManager.FindByEmailAsync(request.Email!) is null ? false : true;
        if (existUserByEmail)
        {
            throw new BadRequestException("Email already exists");
        }
        var existUserByUsername = await _userManager.FindByNameAsync(request.Username!) is null ? false : true;
        if (existUserByUsername)
        {
            throw new BadRequestException("Username already exists");
        }

        var user = new Usuario
        {
            Nombre = request.Nombre,
            Apellido = request.Apellido,
            UserName = request.Username,
            Email = request.Email,
            Telefono = request.Telefono,
            AvatarUrl = request.FotoUrl
        };
        var result = await _userManager.CreateAsync(user!, request.Password!);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, AppRole.GenericUser);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponse
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Telefono = user.Telefono,
                Email = user.Email,
                Username = user.UserName,
                Avatar = user.AvatarUrl,
                Token = _authService.CreateToken(user, roles),
                Roles = roles
            };
        }
        throw new Exception("No se registro el usuario");

    }
}