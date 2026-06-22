using CKD_AI_CDSS.Application.Features.Auth.Commands.Login;
using CKD_AI_CDSS.Application.Features.Auth.Commands.Logout;
using CKD_AI_CDSS.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
        => Ok(await _mediator.Send(command));

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
        => Ok(await _mediator.Send(command));

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutCommand command)
        => Ok(await _mediator.Send(command));
}