using CKD_AI_CDSS.Domain.Enums;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetLabReports;

public class LabReportResponse
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string FilePath { get; set; } = string.Empty;

    public ProcessingStatus Status { get; set; }
}