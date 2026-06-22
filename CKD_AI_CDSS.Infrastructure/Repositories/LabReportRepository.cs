using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Repositories;

public class LabReportRepository : ILabReportRepository
{
    private readonly ApplicationDbContext _context;

    public LabReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> UploadLabReportAsync(LabReport entity, CancellationToken cancellationToken)
    {
        await _context.LabReports.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task<List<LabReport>> GetLabReportsAsync(int patientId, CancellationToken cancellationToken)
    {
        return await _context.LabReports
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<LabReport?> GetByIdAsync(int id)
        => await _context.LabReports.FindAsync(id);

    public async Task<List<LabReport>> GetByPatientIdAsync(int patientId)
        => await _context.LabReports
            .Where(x => x.PatientId == patientId)
            .ToListAsync();

    public async Task AddAsync(LabReport entity)
        => await _context.LabReports.AddAsync(entity);
}