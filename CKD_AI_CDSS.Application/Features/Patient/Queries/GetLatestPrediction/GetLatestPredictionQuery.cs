using CKD_AI_CDSS.Application.Features.Patient.Queries.GetProfile;
using MediatR;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetLatestPrediction;

public record GetLatestPredictionQuery(int UserId)
    : IRequest<PatientProfileResponse>;