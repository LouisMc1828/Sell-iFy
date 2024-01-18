using Sellify.Application.Specification;
using Sellify.Domain;

namespace Sellify.Application.Specification.Users;

public class UserForCountingSpecification : BaseSpecification<Usuario>
{
    public UserForCountingSpecification(UserSpecificationParams userParams) : base(
        x =>
        (string.IsNullOrEmpty(userParams.Search) || x.Nombre!.Contains(userParams.Search) ||
        x.Apellido!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
        )
    )
    {
        
    }
}