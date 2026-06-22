using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CKD_AI_CDSS.Domain.Entities;

namespace CKD_AI_CDSS.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Patient> Patients { get; }
        DbSet<Doctor> Doctors { get; }
        DbSet<LabReport> LabReports { get; }
        DbSet<RetinalImage> RetinalImages { get; }
        DbSet<RetinalAnalysis> RetinalAnalyses { get; }
        DbSet<Prediction> Predictions { get; }
        DbSet<MonitoringHistory> MonitoringHistories { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<ChatbotSession> ChatbotSessions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}