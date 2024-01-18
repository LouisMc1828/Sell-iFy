using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Exceptions;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.ResetPasswordByToken;

public class ResetPasswordByTokenCommandHandler : IRequestHandler<ResetPasswordByTokenCommand, string>
{

    private readonly UserManager<Usuario> _userManager;

    public ResetPasswordByTokenCommandHandler(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> Handle(ResetPasswordByTokenCommand request, CancellationToken cancellationToken)
    {
        if(!string.Equals(request.Password, request.ConfirmPassword))
        {
            throw new BadRequestException("Password not confirmed");
        }

        var updateUser = await _userManager.FindByEmailAsync(request.Email!);
        if(updateUser is null)
        {
            throw new BadRequestException("Email not found");
        }

        var token = Convert.FromBase64String(request.Token!);
        var tokenResult = Encoding.UTF8.GetString(token);

        var resetResult = await _userManager.ResetPasswordAsync(updateUser, tokenResult, request.Password!);
        if (!resetResult.Succeeded)
        {
            throw new Exception("Could not reset password");
        }

        return $"Password updated successfully ${request.Email}";
    }
}