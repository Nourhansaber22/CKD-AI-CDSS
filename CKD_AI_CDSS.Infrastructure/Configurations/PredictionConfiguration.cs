using CKD_AI_CDSS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Infrastructure.Configurations
{
    public class PredictionConfiguration : IEntityTypeConfiguration<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.ToTable("Predictions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RiskLevel)
                .HasConversion<string>();

            builder.Property(x => x.ProbabilityScore)
                .HasColumnType("decimal(5,4)");

            builder.Property(x => x.ProgressionEstimate)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.ShapValues)
                .HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Predictions)
                .HasForeignKey(x => x.PatientId);

            builder.HasOne(x => x.LabReport)
                .WithMany()
                .HasForeignKey(x => x.LabReportId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.RetinalAnalysis)
                .WithMany()
                .HasForeignKey(x => x.RetinalAnalysisId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ReviewedByDoctor)
                .WithMany(x => x.ReviewedPredictions)
                .HasForeignKey(x => x.ReviewedBy)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
