using CKD_AI_CDSS.Application.Features.Auth.Commands.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CKD_AI_CDSS.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class RefreshTokenController : ControllerBase
{
    private readonly IMediator _mediator;

    public RefreshTokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
        RefreshTokenCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }
}