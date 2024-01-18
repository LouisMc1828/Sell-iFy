using MediatR;
using Microsoft.AspNetCore.Http;
using Sellify.Application.Features.Auths.Users.Vms;

namespace Sellify.Application.Features.Auths.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<AuthResponse>
{
    public string? Nombre { get; set;}

    public string? Apellido { get; set;}

    public string? Email { get; set;}

    public string? Telefono { get; set;}

    public IFormFile? Foto { get; set;}

    public string? FotoUrl { get; set;}

    public string? FotoId { get; set;}
}