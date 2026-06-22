using MediatR;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetLabReports;

public record GetLabReportsQuery(int UserId)
    : IRequest<List<LabReportResponse>>;