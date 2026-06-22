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
    public class PatientConfiguration
     : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Gender)
                .HasConversion<string>();

            builder.Property(x => x.Weight)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.Height)
                .HasColumnType("decimal(5,2)");

            builder.Property(x => x.BMI)
                .HasColumnType("decimal(5,2)");
        }
    }
}
