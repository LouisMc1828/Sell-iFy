using FluentValidation;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().WithMessage("Debe ingresar un nombre");
        RuleFor(x => x.Apellido).NotEmpty().WithMessage("Debe ingresar un nombre");
        
    }
}