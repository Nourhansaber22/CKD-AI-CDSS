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
    public class ChatbotSessionConfiguration : IEntityTypeConfiguration<ChatbotSession>
    {
        public void Configure(EntityTypeBuilder<ChatbotSession> builder)
        {
            builder.ToTable("ChatbotSessions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.Response)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.ChatbotSessions)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
