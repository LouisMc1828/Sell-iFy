using Sellify.Domain;

namespace Sellify.Application.Contracts.Identity;

public interface IAuthService
{
    string GetSessionUser();

    string CreateToken(Usuario user, IList<string>? roles);
}