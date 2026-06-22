using CKD_AI_CDSS.Application.Features.Patient.Commands.UpdateProfile;
using CKD_AI_CDSS.Application.Features.Patient.Commands.UploadLabReport;
using CKD_AI_CDSS.Application.Features.Patient.Queries.GetLabReports;
using CKD_AI_CDSS.Application.Features.Patient.Queries.GetLatestPrediction;
using CKD_AI_CDSS.Application.Features.Patient.Queries.GetMonitoringHistory;
using CKD_AI_CDSS.Application.Features.Patient.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CKD_AI_CDSS.API.Controllers;

[ApiController]
[Route("api/v1/patient")]
[Authorize(Roles = "Patient")]
public class PatientController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private int GetUserId()
    {
        return int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    // ====================================
    // GET PROFILE
    // ====================================

    [HttpGet("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile()
    {
        var result = await _mediator.Send(
            new GetPatientProfileQuery(GetUserId()));

        return Ok(result);
    }

    // ====================================
    // UPDATE PROFILE
    // ====================================

    [HttpPut("profile")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdatePatientProfileCommand command)
    {
        command.UserId = GetUserId();

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // ====================================
    // UPLOAD LAB REPORT
    // ====================================

    [HttpPost("lab-report")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UploadLabReport(
        [FromForm] UploadLabReportCommand command)
    {
        command.UserId = GetUserId();

        var result = await _mediator.Send(command);

        return StatusCode(StatusCodes.Status201Created,
            new
            {
                LabReportId = result
            });
    }

    // ====================================
    // GET LAB REPORTS
    // ====================================

    [HttpGet("lab-reports")]
    public async Task<IActionResult> GetLabReports()
    {
        var result = await _mediator.Send(
            new GetLabReportsQuery(GetUserId()));

        return Ok(result);
    }

    [HttpGet("prediction/latest")]
    public async Task<IActionResult> GetLatestPrediction()
    {
        var result = await _mediator.Send(
            new GetLatestPredictionQuery(GetUserId()));

        return Ok(result);
    }

    [HttpGet("monitoring-history")]
    public async Task<IActionResult> GetMonitoringHistory()
    {
        var result = await _mediator.Send(
            new GetMonitoringHistoryQuery(GetUserId()));

        return Ok(result);
    }
}