using CKD_AI_CDSS.Application.Features.Doctor.Commands.ExportPdfReport;
using CKD_AI_CDSS.Application.Features.Doctor.Commands.ReviewReport;
using CKD_AI_CDSS.Application.Features.Doctor.Commands.UploadRetinalImage;
using CKD_AI_CDSS.Application.Features.Doctor.Queries.GetClinicalReport;
using CKD_AI_CDSS.Application.Features.Doctor.Queries.GetPatientDetails;
using CKD_AI_CDSS.Application.Features.Doctor.Queries.GetPatientMonitoringHistory;
using CKD_AI_CDSS.Application.Features.Doctor.Queries.SearchPatients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CKD_AI_CDSS.API.Controllers;

[ApiController]
[Route("api/v1/doctor")]
[Authorize(Roles = "DOCTOR")]
public class DoctorController : ControllerBase
{
    private readonly IMediator _mediator;

    public DoctorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ================= SEARCH PATIENTS =================
    // GET: api/v1/doctor/patients?q=ahmed
    [HttpGet("patients")]
    public async Task<IActionResult> SearchPatients(
        [FromQuery] string q,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new SearchPatientsQuery(q),
            cancellationToken);

        return Ok(result);
    }

    // ================= PATIENT DETAILS =================
    // GET: api/v1/doctor/patients/5
    [HttpGet("patients/{id}")]
    public async Task<IActionResult> GetPatientDetails(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetPatientDetailsQuery(id),
            cancellationToken);

        return Ok(result);
    }

    // ================= MONITORING HISTORY =================
    // GET: api/v1/doctor/patients/5/monitoring-history
    [HttpGet("patients/{id}/monitoring-history")]
    public async Task<IActionResult> GetMonitoringHistory(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetPatientMonitoringHistoryQuery(id),
            cancellationToken);

        return Ok(result);
    }

    // ================= RETINAL IMAGE =================
    // POST: api/v1/doctor/retinal-image
    [HttpPost("retinal-image")]
    public async Task<IActionResult> UploadRetinalImage(
        [FromForm] UploadRetinalImageCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    // ================= FULL REPORT =================
    // GET: api/v1/doctor/report/1/10
    [HttpGet("report/{patientId}/{predictionId}")]
    public async Task<IActionResult> GetFullReport(
        int patientId,
        int predictionId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new GetClinicalReportQuery(
                patientId,
                predictionId),
            cancellationToken);

        return Ok(result);
    }

    // ================= REVIEW REPORT =================
    // PATCH: api/v1/doctor/report/10/review
    [HttpPatch("report/{predictionId}/review")]
    public async Task<IActionResult> ReviewReport(
        int predictionId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ReviewReportCommand(predictionId),
            cancellationToken);

        return Ok(result);
    }

    // ================= EXPORT PDF =================
    // GET: api/v1/doctor/report/10/export-pdf
    [HttpGet("report/{predictionId}/export-pdf")]
    public async Task<IActionResult> ExportPdf(
     int predictionId,
     CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ExportPdfReportQuery(predictionId),
            cancellationToken);

        return File(
            result.FileContents,
            "application/pdf",
            result.FileName);
    }
}