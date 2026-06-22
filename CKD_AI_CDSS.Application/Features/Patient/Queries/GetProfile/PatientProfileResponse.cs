using CKD_AI_CDSS.Domain.Enums;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetProfile;

public class PatientProfileResponse
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public string? MedicalHistory { get; set; }

    public string? Symptoms { get; set; }

    public string? LifestyleInfo { get; set; }
}