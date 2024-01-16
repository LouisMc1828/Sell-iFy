using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sellify.Application.Contracts.Infrasctructure;
using Sellify.Application.Features.Auths.Users.Commands.LoginUser;
using Sellify.Application.Features.Auths.Users.Vms;

namespace Sellify.Api.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private IMediator _mediator;
    private IManageImageService _manageImageService;

    public UserController(IMediator mediator, IManageImageService manageImageService)
    {
        _mediator = mediator;
        _manageImageService = manageImageService;
    }

    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUserCommand request)
    {
        return await _mediator.Send(request);
    }
}