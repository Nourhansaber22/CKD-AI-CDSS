using CKD_AI_CDSS.Application.Features.Admin.Commands.ActivateUser;
using CKD_AI_CDSS.Application.Features.Admin.Commands.DeactivateUser;
using CKD_AI_CDSS.Application.Features.Admin.Commands.UpdateUser;
using CKD_AI_CDSS.Application.Features.Admin.Queries.GetAuditLogs;
using CKD_AI_CDSS.Application.Features.Admin.Queries.GetSystemMetrics;
using CKD_AI_CDSS.Application.Features.Admin.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CKD_AI_CDSS.API.Controllers;

[ApiController]
[Route("api/v1/admin")]
[Authorize(Roles = "ADMIN")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ==================================================
    // GET: api/v1/admin/users
    // ==================================================
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] GetUsersQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    // ==================================================
    // PATCH: api/v1/admin/users/{id}
    // ==================================================
    [HttpPatch("users/{id:int}")]
    public async Task<IActionResult> UpdateUser(
    int id,
    [FromBody] UpdateUserRequest request)
    {
        var command = new UpdateUserCommand(
            id,
            request.Name,
            request.Email,
            request.Role);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // ==================================================
    // PATCH: api/v1/admin/users/{id}/deactivate
    // ==================================================
    [HttpPatch("users/{id:int}/deactivate")]
    public async Task<IActionResult> DeactivateUser(
        int id)
    {
        var result = await _mediator.Send(
            new DeactivateUserCommand(id)
            );

        return Ok(result);
    }

    // ==================================================
    // PATCH: api/v1/admin/users/{id}/activate
    // ==================================================
    [HttpPatch("users/{id:int}/activate")]
    public async Task<IActionResult> ActivateUser(
        int id)
    {
        var result = await _mediator.Send(
            new ActivateUserCommand(id)
             );

        return Ok(result);
    }

    // ==================================================
    // GET: api/v1/admin/audit-logs
    // ==================================================
    [HttpGet("audit-logs")]
    public async Task<IActionResult> GetAuditLogs(
        [FromQuery] GetAuditLogsQuery query)
    {
        var result = await _mediator.Send(query);

        return Ok(result);
    }

    // ==================================================
    // GET: api/v1/admin/system-metrics
    // ==================================================
    [HttpGet("system-metrics")]
    public async Task<IActionResult> GetSystemMetrics()
    {
        var result = await _mediator.Send(
            new GetSystemMetricsQuery());

        return Ok(result);
    }
}