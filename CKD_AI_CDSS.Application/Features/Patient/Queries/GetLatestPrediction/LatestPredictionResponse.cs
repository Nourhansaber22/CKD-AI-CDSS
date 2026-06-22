using CKD_AI_CDSS.Domain.Enums;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetLatestPrediction;

public class LatestPredictionResponse
{
    public int PredictionId { get; set; }

    public RiskLevel RiskLevel { get; set; }

    public decimal ProbabilityScore { get; set; }

    public string? ProgressionEstimate { get; set; }

    public string? ShapValues { get; set; }

    public DateTime CreatedAt { get; set; }
}