using MediatR;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetProfile;

public record GetPatientProfileQuery(int UserId)
    : IRequest<PatientProfileResponse>;