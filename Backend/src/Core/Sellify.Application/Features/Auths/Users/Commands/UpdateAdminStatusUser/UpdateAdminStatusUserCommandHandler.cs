using MediatR;
using Microsoft.AspNetCore.Identity;
using Sellify.Application.Exceptions;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;

public class UpdateAdminStatusUserCommandHandler : IRequestHandler<UpdateAdminStatusUserCommand, Usuario>
{

    private readonly UserManager<Usuario> _userManager;

    public UpdateAdminStatusUserCommandHandler(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Usuario> Handle(UpdateAdminStatusUserCommand request, CancellationToken cancellationToken)
    {
        var updateUser = await _userManager.FindByIdAsync(request.Id!);
        if (updateUser is null)
        {
            throw new BadRequestException("Could not find user");
        }

        updateUser.IsActive = !updateUser.IsActive;

        var result = await _userManager.UpdateAsync(updateUser);
        if (!result.Succeeded)
        {
            throw new Exception("Could not update status of user");
        }

        return updateUser;
    }
}
