using FluentValidation.Results;

namespace Sellify.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; }
    public ValidationException() : base("Uno o varios errores estan presentes en la validacion")
    {
        Errors = new Dictionary<string, string[]>();
    }
    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e=> e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}