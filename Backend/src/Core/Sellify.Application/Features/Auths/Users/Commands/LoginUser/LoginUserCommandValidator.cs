using FluentValidation;

namespace Sellify.Application.Features.Auths.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{

    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Por favor ingrese el email");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Por favor ingrese el password");
    }
}