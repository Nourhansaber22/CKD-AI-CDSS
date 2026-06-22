using CKD_AI_CDSS.Application.Features.Doctor.Queries.GetPatientDetails;
using CKD_AI_CDSS.Application.Features.Doctor.Queries.GetPatientMonitoringHistory;
using CKD_AI_CDSS.Application.Features.Doctor.Queries.SearchPatients;
using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // ================= SEARCH PATIENTS =================
    public async Task<List<PatientSearchResponse>> SearchPatientsAsync(
        string query,
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(x => x.Name.Contains(query))
            .Select(x => new PatientSearchResponse
            {
                PatientId = x.Id,
                Name = x.Name,
                Age = x.Age,
                Gender = x.Gender.HasValue ? x.Gender.ToString() : null,

                LatestRiskLevel = null,
                LastPredictionDate = null
            })
            .ToListAsync(cancellationToken);
    }

    // ================= PATIENT DETAILS =================
    public async Task<PatientDetailsResponse?> GetPatientDetailsAsync(
        int patientId,
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .Where(x => x.Id == patientId)
            .Select(x => new PatientDetailsResponse
            {
                PatientId = x.Id,
                Name = x.Name,
                Age = x.Age,
                Gender = x.Gender.HasValue ? x.Gender.ToString() : null,
                MedicalHistory = x.MedicalHistory,
                Symptoms = x.Symptoms,
                LifestyleInfo = x.LifestyleInfo
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    // ================= MONITORING HISTORY =================
    public async Task<List<PatientMonitoringHistoryResponse>> GetPatientMonitoringHistoryAsync(
        int patientId,
        CancellationToken cancellationToken)
    {
        return await _context.MonitoringHistories
            .AsNoTracking()
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.RecordedAt)
            .Select(x => new PatientMonitoringHistoryResponse
            {
                PredictionId = x.PredictionId,
                RiskLevel = x.RiskLevel.ToString(),
                TrendIndicator = x.TrendIndicator.ToString(),
                RecordedAt = x.RecordedAt
            })
            .ToListAsync(cancellationToken);
    }

    // ================= BASIC CRUD =================
    public async Task<Patient?> GetByIdAsync(
        int patientId,
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == patientId, cancellationToken);
    }

    public async Task<List<Patient>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Patient patient,
        CancellationToken cancellationToken)
    {
        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Patient patient,
        CancellationToken cancellationToken)
    {
        _context.Patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(
        int patientId,
        CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(x => x.Id == patientId, cancellationToken);

        if (patient is null)
            return;

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        int patientId,
        CancellationToken cancellationToken)
    {
        return await _context.Patients
            .AnyAsync(x => x.Id == patientId, cancellationToken);
    }

    // ================= MONITORING HISTORY (MISSING - FIXED) =================
    public async Task AddMonitoringHistoryAsync(
        MonitoringHistory history,
        CancellationToken cancellationToken)
    {
        await _context.MonitoringHistories.AddAsync(history, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}