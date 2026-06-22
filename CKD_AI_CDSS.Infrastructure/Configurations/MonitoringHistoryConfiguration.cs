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
    public class MonitoringHistoryConfiguration : IEntityTypeConfiguration<MonitoringHistory>
    {
        public void Configure(EntityTypeBuilder<MonitoringHistory> builder)
        {
            builder.ToTable("MonitoringHistory");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RiskLevelSnapshot)
                .HasConversion<string>();

            builder.Property(x => x.TrendIndicator)
                .HasConversion<string>();

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.MonitoringHistories)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Prediction)
                .WithMany()
                .HasForeignKey(x => x.PredictionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
