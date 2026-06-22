using CKD_AI_CDSS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetMonitoringHistory;

public class GetMonitoringHistoryQueryHandler
    : IRequestHandler<
        GetMonitoringHistoryQuery,
        List<MonitoringHistoryDto>>
{
    private readonly IApplicationDbContext _context;

    public GetMonitoringHistoryQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MonitoringHistoryDto>> Handle(
        GetMonitoringHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(
                x => x.UserId == request.UserId,
                cancellationToken);

        if (patient is null)
            throw new Exception("Patient not found");

        return await _context.MonitoringHistories
            .Where(x => x.PatientId == patient.Id)
            .OrderByDescending(x => x.RecordedAt)
            .Select(x => new MonitoringHistoryDto
            {
                Id = x.Id,
                PredictionId = x.PredictionId,
                RecordedAt = x.RecordedAt,
                RiskLevelSnapshot = x.RiskLevelSnapshot,
                TrendIndicator = x.TrendIndicator
            })
            .ToListAsync(cancellationToken);
    }
}