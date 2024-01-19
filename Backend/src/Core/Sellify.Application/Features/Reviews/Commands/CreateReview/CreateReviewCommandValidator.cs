using FluentValidation;

namespace Sellify.Application.Features.Reviews.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre no puede ser nulo");

        RuleFor(x => x.Comentario)
            .NotEmpty().WithMessage("El Comentario no puede ser nulo");

        RuleFor(x => x.Rating)
            .NotEmpty().WithMessage(" El Rating no puede ser nulo");

    }
}