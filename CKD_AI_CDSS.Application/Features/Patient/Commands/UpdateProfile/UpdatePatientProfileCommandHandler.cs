using CKD_AI_CDSS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Patient.Commands.UpdateProfile;

public class UpdatePatientProfileCommandHandler
    : IRequestHandler<UpdatePatientProfileCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdatePatientProfileCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(
        UpdatePatientProfileCommand request,
        CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(
                x => x.UserId == request.UserId,
                cancellationToken);

        if (patient is null)
            throw new Exception("Patient not found");

        patient.Age = request.Age;
        patient.Gender = request.Gender;
        patient.Weight = request.Weight;
        patient.Height = request.Height;
        patient.MedicalHistory = request.MedicalHistory;
        patient.Symptoms = request.Symptoms;
        patient.LifestyleInfo = request.LifestyleInfo;

        if (patient.Weight.HasValue &&
            patient.Height.HasValue &&
            patient.Height > 0)
        {
            var heightInMeters =
                patient.Height.Value / 100m;

            patient.BMI =
                patient.Weight.Value /
                (heightInMeters * heightInMeters);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}