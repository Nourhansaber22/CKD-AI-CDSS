using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Domain.Enums;
using MediatR;

namespace CKD_AI_CDSS.Application.Features.Patient.Commands.UploadLabReport;

public class UploadLabReportCommandHandler
    : IRequestHandler<UploadLabReportCommand, int>
{
    private readonly IApplicationDbContext _context;

    public UploadLabReportCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        UploadLabReportCommand request,
        CancellationToken cancellationToken)
    {
        var uploadsFolder =
            Path.Combine(Directory.GetCurrentDirectory(),
            "Uploads", "LabReports");

        Directory.CreateDirectory(uploadsFolder);

        var fileName =
            $"{Guid.NewGuid()}_{request.File.FileName}";

        var filePath =
            Path.Combine(uploadsFolder, fileName);

        using var stream =
            new FileStream(filePath, FileMode.Create);

        await request.File.CopyToAsync(stream,
            cancellationToken);

        var report = new LabReport
        {
            PatientId = request.PatientId,
            FilePath = filePath,
            Status = ProcessingStatus.Pending
        };

        _context.LabReports.Add(report);

        await _context.SaveChangesAsync(
            cancellationToken);

        return report.Id;
    }
}