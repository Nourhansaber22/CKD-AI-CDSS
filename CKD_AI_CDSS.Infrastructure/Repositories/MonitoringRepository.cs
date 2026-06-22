using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Repositories;

public class MonitoringRepository : IMonitoringRepository
{
    private readonly ApplicationDbContext _context;

    public MonitoringRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MonitoringHistory>> GetMonitoringHistoryAsync(
        int patientId,
        CancellationToken cancellationToken)
    {
        return await _context.MonitoringHistories
            .Where(x => x.PatientId == patientId)
            .OrderByDescending(x => x.RecordedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<MonitoringHistory>> GetByPatientIdAsync(int patientId)
    {
        return await _context.MonitoringHistories
            .Where(x => x.PatientId == patientId)
            .ToListAsync();
    }

    public async Task AddAsync(MonitoringHistory entity)
    {
        await _context.MonitoringHistories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
}