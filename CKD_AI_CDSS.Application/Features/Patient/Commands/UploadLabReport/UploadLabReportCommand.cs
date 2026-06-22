using MediatR;
using Microsoft.AspNetCore.Http;

namespace CKD_AI_CDSS.Application.Features.Patient.Commands.UploadLabReport;

public class UploadLabReportCommand : IRequest<int>
{
    public int UserId { get; set; }

    public IFormFile File { get; set; } = null!;
}