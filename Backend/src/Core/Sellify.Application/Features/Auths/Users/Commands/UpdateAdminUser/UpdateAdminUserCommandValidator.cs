using FluentValidation;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateAdminUser;

public class UpdateAdminUserCommandValidator : AbstractValidator<UpdateAdminUserCommand>
{

    public UpdateAdminUserCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Please enter a valid");

        RuleFor(x => x.Apellido).NotEmpty().WithMessage("Please enter a valid");

        RuleFor(x => x.Telefono).NotEmpty().WithMessage("Please enter a valid");
    }
}