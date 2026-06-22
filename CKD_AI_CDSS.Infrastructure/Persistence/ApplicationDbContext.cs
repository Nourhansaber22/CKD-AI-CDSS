using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // ================= DBSets =================
    public DbSet<User> Users => Set<User>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<LabReport> LabReports => Set<LabReport>();
    public DbSet<RetinalImage> RetinalImages => Set<RetinalImage>();
    public DbSet<RetinalAnalysis> RetinalAnalyses => Set<RetinalAnalysis>();
    public DbSet<Prediction> Predictions => Set<Prediction>();
    public DbSet<MonitoringHistory> MonitoringHistories => Set<MonitoringHistory>();
    public DbSet<ChatbotSession> ChatbotSessions => Set<ChatbotSession>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Notification> Notifications => Set<Notification>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ================= CONFIGURATIONS =================
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UserConfiguration).Assembly);

        // ================= USER ↔ PATIENT (1:1) =================
        modelBuilder.Entity<User>()
            .HasOne(u => u.Patient)
            .WithOne(p => p.User)
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ================= USER ↔ DOCTOR (1:1) =================
        modelBuilder.Entity<User>()
            .HasOne(u => u.Doctor)
            .WithOne(d => d.User)
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ================= PATIENT → PREDICTIONS =================
        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.Patient)
            .WithMany(p => p.Predictions)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // ================= LABREPORT → PREDICTIONS =================
        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.LabReport)
            .WithMany()
            .HasForeignKey(p => p.LabReportId)
            .OnDelete(DeleteBehavior.Restrict);

        // ================= RETINAL → PREDICTIONS =================
        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.RetinalAnalysis)
            .WithMany()
            .HasForeignKey(p => p.RetinalAnalysisId)
            .OnDelete(DeleteBehavior.Restrict);

        // ================= LABREPORT → PATIENT =================
        modelBuilder.Entity<LabReport>()
            .HasOne(l => l.Patient)
            .WithMany(p => p.LabReports)
            .HasForeignKey(l => l.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // ================= RETINALIMAGE → PATIENT =================
        modelBuilder.Entity<RetinalImage>()
            .HasOne(r => r.Patient)
            .WithMany(p => p.RetinalImages)
            .HasForeignKey(r => r.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // ================= PREDICTION → DOCTOR REVIEW =================
        modelBuilder.Entity<Prediction>()
            .HasOne(p => p.ReviewedByDoctor)
            .WithMany(d => d.ReviewedPredictions)
            .HasForeignKey(p => p.ReviewedBy)
            .OnDelete(DeleteBehavior.SetNull);

        // ================= REFRESH TOKEN =================
        modelBuilder.Entity<RefreshToken>()
            .HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ================= DECIMAL PRECISION =================
        modelBuilder.Entity<LabReport>()
            .Property(x => x.Creatinine).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Egfr).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Glucose).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Hemoglobin).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Albumin).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Sodium).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Potassium).HasPrecision(6, 2);

        modelBuilder.Entity<LabReport>()
            .Property(x => x.Urea).HasPrecision(6, 2);

        modelBuilder.Entity<Prediction>()
            .Property(x => x.ProbabilityScore)
            .HasPrecision(5, 4);

        // ================= PERFORMANCE INDEXES (IMPORTANT) =================
        modelBuilder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<Patient>()
            .HasIndex(x => x.UserId);

        modelBuilder.Entity<Prediction>()
            .HasIndex(x => x.PatientId);

        modelBuilder.Entity<LabReport>()
            .HasIndex(x => x.PatientId);

        modelBuilder.Entity<RetinalImage>()
            .HasIndex(x => x.PatientId);

        modelBuilder.Entity<RefreshToken>()
            .HasIndex(x => x.Token)
            .IsUnique();
    }
}