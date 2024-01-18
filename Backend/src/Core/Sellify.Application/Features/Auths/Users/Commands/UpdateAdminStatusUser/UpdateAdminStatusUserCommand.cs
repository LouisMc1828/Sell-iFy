using MediatR;
using Sellify.Domain;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateAdminStatusUser;

public class UpdateAdminStatusUserCommand : IRequest<Usuario>
{
    public string? Id { get; set; }
}