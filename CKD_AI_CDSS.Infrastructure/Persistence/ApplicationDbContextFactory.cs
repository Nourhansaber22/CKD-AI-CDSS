using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CKD_AI_CDSS.Infrastructure.Persistence;

public class ApplicationDbContextFactory
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-FPF55AM\\MSSQLSERVER01;Database=CKD_AI_CDSS;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}