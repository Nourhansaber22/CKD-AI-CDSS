using CKD_AI_CDSS.Application.Features.Doctor.Queries.GetClinicalReport;
using CKD_AI_CDSS.Application.Features.Prediction.Queries.GetLatestPrediction;
using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class PredictionRepository : IPredictionRepository
{
    private readonly ApplicationDbContext _context;

    public PredictionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClinicalReportResponse?> GetClinicalReportAsync(
     int patientId,
     int predictionId,
     CancellationToken cancellationToken)
    {
        var prediction = await _context.Predictions
            .FirstOrDefaultAsync(x =>
                x.Id == predictionId &&
                x.PatientId == patientId,
                cancellationToken);

        if (prediction == null)
            return null;

        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.Id == patientId, cancellationToken);

        return new ClinicalReportResponse
        {
            PatientId = patientId,
            PatientName = patient?.Name ?? "",
            PredictionId = predictionId,
            RiskLevel = prediction.RiskLevel.ToString(),
            ProbabilityScore = prediction.ProbabilityScore
        };
    }
    public async Task<DateTime> ReviewReportAsync(
    int predictionId,
    CancellationToken cancellationToken)
    {
        var prediction = await _context.Predictions
            .FirstOrDefaultAsync(x => x.Id == predictionId, cancellationToken);

        if (prediction == null)
            return DateTime.UtcNow;

        prediction.UpdatedAt = DateTime.UtcNow;

        _context.Predictions.Update(prediction);
        await _context.SaveChangesAsync(cancellationToken);

        return prediction.UpdatedAt;
    }

    public async Task<PredictionResponse?> GetLatestPredictionAsync(
     int patientId,
     CancellationToken cancellationToken)
    {
        var prediction = await _context.Predictions
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (prediction == null)
            return null;

        return new PredictionResponse
        {
            PredictionId = prediction.Id,
            PatientId = prediction.PatientId,
            RiskLevel = prediction.RiskLevel.ToString(),
            ProbabilityScore = prediction.ProbabilityScore,
            CreatedAt = prediction.CreatedAt
        };

    }
    public async Task AddAsync(Prediction prediction, CancellationToken cancellationToken)
    {
        await _context.Predictions.AddAsync(prediction, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddMonitoringHistoryAsync(MonitoringHistory history, CancellationToken cancellationToken)
    {
        await _context.MonitoringHistories.AddAsync(history, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}