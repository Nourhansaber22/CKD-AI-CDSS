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
    public class RetinalAnalysisConfiguration : IEntityTypeConfiguration<RetinalAnalysis>
    {
        public void Configure(EntityTypeBuilder<RetinalAnalysis> builder)
        {
            builder.ToTable("RetinalAnalysis");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Features)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.FeatureImportance)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.HeatmapPath)
                .HasMaxLength(500);

            builder.HasOne(x => x.RetinalImage)
                .WithOne(x => x.RetinalAnalysis)
                .HasForeignKey<RetinalAnalysis>(x => x.RetinalImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
