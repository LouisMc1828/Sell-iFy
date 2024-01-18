using Sellify.Application.Specification;
using Sellify.Domain;

namespace Sellify.Application.Specification.Users;

public class UserSpecification : BaseSpecification<Usuario>
{
    public UserSpecification(UserSpecificationParams userParams) : base(
        x =>
        (string.IsNullOrEmpty(userParams.Search) || x.Nombre!.Contains(userParams.Search) ||
        x.Apellido!.Contains(userParams.Search) || x.Email!.Contains(userParams.Search)
        )
    )
    {
        ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);
        if (!string.IsNullOrEmpty(userParams.Sort))
        {
            switch (userParams.Sort)
            {
                case "nombreAsc":
                    AddOrderBy(y => y.Nombre!);
                break;

                case "nombreDesc":
                    AddOrderByDescending(y => y.Nombre!);
                break;

                case "apellidoAsc":
                    AddOrderBy(y => y.Apellido!);
                break;

                case "apellidoDesc":
                    AddOrderByDescending(y => y.Apellido!);
                break;

                default:
                    AddOrderBy(y => y.Nombre!);
                break;
            }
        }
        else
        {
            AddOrderByDescending(y => y.Nombre!);
        }
    }
}