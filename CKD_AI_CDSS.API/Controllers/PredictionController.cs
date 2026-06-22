using CKD_AI_CDSS.Application.Features.Prediction.Commands.CreatePrediction;
using CKD_AI_CDSS.Application.Features.Prediction.Queries.GetLatestPrediction;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CKD_AI_CDSS.API.Controllers;

[ApiController]
[Route("api/v1/predictions")]
[Authorize]
public class PredictionController : ControllerBase
{
    private readonly IMediator _mediator;

    public PredictionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST api/v1/predictions
    [Authorize(Roles = "PATIENT")]
    [HttpPost]
    public async Task<IActionResult> CreatePrediction(
        CreatePredictionCommand command)
    {
        var result = await _mediator.Send(command);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // GET api/v1/predictions/latest
    [Authorize(Roles = "PATIENT")]
    [HttpGet("latest")]
    public async Task<IActionResult> GetLatestPrediction()
    {
        var patientId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _mediator.Send(
            new GetLatestPredictionQuery(patientId));

        return Ok(result);
    }
}