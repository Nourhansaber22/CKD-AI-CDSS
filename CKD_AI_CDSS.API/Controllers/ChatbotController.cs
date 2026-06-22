using CKD_AI_CDSS.Application.Features.Chatbot.Commands.SendMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CKD_AI_CDSS.API.Controllers;

[ApiController]
[Route("api/v1/chatbot")]
[Authorize(Roles = "Patient")]
public class ChatbotController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatbotController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("message")]
    public async Task<IActionResult> SendMessage(
        [FromBody] SendMessageRequest request)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(
            new SendMessageCommand(
                userId,
                request.Message));

        return Ok(result);
    }
}