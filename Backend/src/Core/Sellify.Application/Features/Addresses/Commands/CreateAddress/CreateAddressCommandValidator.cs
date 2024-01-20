using FluentValidation;

namespace Sellify.Application.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(x => x.Direccion).NotEmpty().WithMessage(" no puede ser null");

        RuleFor(x => x.Ciudad).NotEmpty().WithMessage("no puede ser null");

        RuleFor(x => x.Departamento).NotEmpty().WithMessage("no puede ser null");

        RuleFor(x => x.CodigoPostal).NotEmpty().WithMessage("no puede ser null");

        RuleFor(x => x.Pais).NotEmpty().WithMessage("no puede ser null");
    }
}