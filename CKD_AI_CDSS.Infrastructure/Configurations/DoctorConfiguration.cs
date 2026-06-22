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
    public class DoctorConfiguration
    : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Specialization)
                .HasMaxLength(255);

            builder.Property(x => x.ClinicName)
                .HasMaxLength(255);
        }
    }
}
