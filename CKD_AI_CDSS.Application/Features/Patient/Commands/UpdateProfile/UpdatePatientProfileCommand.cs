using CKD_AI_CDSS.Domain.Enums;
using MediatR;

namespace CKD_AI_CDSS.Application.Features.Patient.Commands.UpdateProfile;

public class UpdatePatientProfileCommand : IRequest<bool>
{
    public int UserId { get; set; }

    public int? Age { get; set; }

    public Gender? Gender { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Height { get; set; }

    public string? MedicalHistory { get; set; }

    public string? Symptoms { get; set; }

    public string? LifestyleInfo { get; set; }
}