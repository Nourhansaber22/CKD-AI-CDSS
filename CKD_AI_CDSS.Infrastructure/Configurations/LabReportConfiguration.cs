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
    public class LabReportConfiguration
    : IEntityTypeConfiguration<LabReport>
    {
        public void Configure(EntityTypeBuilder<LabReport> builder)
        {
            builder.ToTable("LabReports");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasConversion<string>();

            builder.Property(x => x.FilePath)
                .IsRequired();

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.LabReports)
                .HasForeignKey(x => x.PatientId);
        }
    }
}
