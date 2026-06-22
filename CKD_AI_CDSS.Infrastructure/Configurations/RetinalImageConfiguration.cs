using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Infrastructure.Configurations
{
    public class RetinalImageConfiguration : IEntityTypeConfiguration<RetinalImage>
    {
        public void Configure(EntityTypeBuilder<RetinalImage> builder)
        {
            builder.ToTable("RetinalImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Status)
                .HasConversion<string>()
                .HasDefaultValue(ProcessingStatus.Pending);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.RetinalImages)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.UploadedDoctor)
                .WithMany()
                .HasForeignKey(x => x.UploadedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
