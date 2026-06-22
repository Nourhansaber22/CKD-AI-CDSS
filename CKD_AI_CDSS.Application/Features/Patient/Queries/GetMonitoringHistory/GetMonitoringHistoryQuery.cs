using MediatR;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetMonitoringHistory;

public record GetMonitoringHistoryQuery(int UserId)
    : IRequest<List<MonitoringHistoryDto>>;