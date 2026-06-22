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
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Message))
            return BadRequest("Message cannot be empty.");

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var patientId))
            return Unauthorized("Invalid user id.");

        var command = new SendMessageCommand
        {
            PatientId = patientId,
            Message = request.Message.Trim()
        };

        var result = await _mediator.Send(command);

        return Ok(result);
    }
}