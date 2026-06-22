using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetProfile;

public class GetPatientProfileQueryHandler
    : IRequestHandler<GetPatientProfileQuery, PatientProfileResponse>
{
    private readonly IApplicationDbContext _context;

    public GetPatientProfileQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PatientProfileResponse> Handle(
        GetPatientProfileQuery request,
        CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Include(x => x.User)
            .FirstOrDefaultAsync(
                x => x.UserId == request.UserId,
                cancellationToken);

        if (patient is null)
            throw new Exception("Patient not found");

        return new PatientProfileResponse
        {
            Id = patient.Id,
            Name = patient.User.Name,
            Email = patient.User.Email,
            Age = patient.Age ?? 0,
            Gender = patient.Gender ?? Gender.Male,
            Weight = (double)(patient.Weight ?? 0),
            Height = (double)(patient.Height ?? 0),
            MedicalHistory = patient.MedicalHistory,
            Symptoms = patient.Symptoms,
            LifestyleInfo = patient.LifestyleInfo
        };
    }
}