using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Contracts.Identity;
using Sellify.Application.Exceptions;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.ResetPassword;

public class ResetPasswordCommandaHandler : IRequestHandler<ResetPasswordCommand>
{

    private readonly UserManager<Usuario> _userManager;
    private readonly IAuthService _authService;

    public ResetPasswordCommandaHandler(UserManager<Usuario> userManager, IAuthService authService)
    {
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByNameAsync(_authService.GetSessionUser());
        if (updateUser is null)
        {
            throw new BadRequestException("Couldn't find");
        }

        var resultValidateOldPassword = _userManager.PasswordHasher.VerifyHashedPassword
        (
            updateUser,
            updateUser.PasswordHash!,
            request.OldPassword!
        );
        if (!(resultValidateOldPassword == PasswordVerificationResult.Success))
        {
            throw new BadRequestException("Current password validation failed");
        }

        var hashedNewPassword = _userManager.PasswordHasher.HashPassword(updateUser, request.NewPassword!);
        updateUser.PasswordHash = hashedNewPassword;

        var result = await _userManager.UpdateAsync(updateUser);
        if(!result.Succeeded)
        {
            throw new Exception("Could not reset password");
        }

        return Unit.Value;
    }
}