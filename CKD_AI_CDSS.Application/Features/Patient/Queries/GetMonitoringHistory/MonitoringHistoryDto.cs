using CKD_AI_CDSS.Domain.Enums;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetMonitoringHistory;

public class MonitoringHistoryDto
{
    public int Id { get; set; }

    public int PredictionId { get; set; }

    public DateTime RecordedAt { get; set; }

    public RiskLevel RiskLevelSnapshot { get; set; }

    public TrendIndicator TrendIndicator { get; set; }
}