using FluentValidation;

namespace Sellify.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede ser nulo")
            .MaximumLength(50).WithMessage("El limite es de 50 caracteres");

        RuleFor(x => x.Descripcion)
            .NotEmpty().WithMessage("La descripcion no puede ser nulo");

        RuleFor(x => x.Stock)
            .NotEmpty().WithMessage(" El stock no puede ser nulo");

        RuleFor(x => x.Precio)
            .NotEmpty().WithMessage(" El precio no puede ser nulo");
    }
}