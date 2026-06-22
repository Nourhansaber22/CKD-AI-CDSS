using CKD_AI_CDSS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CKD_AI_CDSS.Infrastructure.Configurations
{
    public class UserConfiguration
     : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Role)
                .HasConversion<string>();

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(x => x.Patient)
                .WithOne(x => x.User)
                .HasForeignKey<Patient>(x => x.UserId);

            builder.HasOne(x => x.Doctor)
                .WithOne(x => x.User)
                .HasForeignKey<Doctor>(x => x.UserId);
        }
    }
}
