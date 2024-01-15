using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Exceptions;
using Sellify.Application.Features.Addresses.Vms;
using Sellify.Application.Features.Auths.Users.Vms;
using Sellify.Application.Persistence;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{

    private readonly UserManager<Usuario> _userManager;

    private readonly SignInManager<Usuario> _signInManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IAuthService _authService;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;


    public LoginUserCommandHandler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager, IAuthService authService, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _authService = authService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        if (user is null)
        {
            throw new NotFoundException(nameof(Usuario), request.Email!);
        }

        if (!user.IsActive)
        {
            throw new Exception($"User {request.Email} is not active");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

        if (!result.Succeeded)
        {
            throw new Exception($"User {request.Email} credentials are incorrect");
        }

        var dirEnvio = await _unitOfWork.Repository<Address>().GetEntityAsync(
            x => x.Username == user.UserName
        );

        var roles = await _userManager.GetRolesAsync(user);

        var authResponse = new AuthResponse{
            Id = user.Id,
            Nombre = user.Nombre,
            Apellido = user.Apellido,
            Telefono = user.Telefono,
            Email = user.Email,
            Username = user.UserName,
            Avatar = user.AvatarUrl,
            Roles = roles,
            DireccionEnvio = _mapper.Map<AddressVm>(dirEnvio),
            Token = _authService.CreateToken(user,roles)
        };

        return authResponse;
    }
}