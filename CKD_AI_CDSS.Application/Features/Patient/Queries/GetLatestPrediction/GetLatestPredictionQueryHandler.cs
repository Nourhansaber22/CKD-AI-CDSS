using CKD_AI_CDSS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetLatestPrediction;

public class GetLatestPredictionQueryHandler
    : IRequestHandler<
        GetLatestPredictionQuery,
        LatestPredictionResponse?>
{
    private readonly IApplicationDbContext _context;

    public GetLatestPredictionQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LatestPredictionResponse?> Handle(
        GetLatestPredictionQuery request,
        CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(
                x => x.UserId == request.UserId,
                cancellationToken);

        if (patient is null)
            throw new Exception("Patient not found");

        var prediction = await _context.Predictions
            .Where(x => x.PatientId == patient.Id)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);

        if (prediction is null)
            return null;

        return new LatestPredictionResponse
        {
            PredictionId = prediction.Id,
            RiskLevel = prediction.RiskLevel,
            ProbabilityScore = prediction.ProbabilityScore,
            ProgressionEstimate = prediction.ProgressionEstimate,
            ShapValues = prediction.ShapValues,
            CreatedAt = prediction.CreatedAt
        };
    }
}