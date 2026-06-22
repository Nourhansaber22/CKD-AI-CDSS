using CKD_AI_CDSS.Application.Features.Admin.Queries.GetSystemMetrics;
using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Repositories;

public class SystemMetricsRepository : ISystemMetricsRepository
{
    private readonly ApplicationDbContext _context;

    public SystemMetricsRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<SystemMetricsResponse> GetSystemMetricsAsync(
        CancellationToken cancellationToken)
    {
        return new SystemMetricsResponse
        {
            TotalUsers = await _context.Users.CountAsync(cancellationToken),

            // مؤقتًا
            ActiveSessions = 0,

            // عدلي اسم DbSet لو مختلف عندك
            PredictionsToday = await _context.Predictions
                .CountAsync(x => x.CreatedAt.Date == DateTime.UtcNow.Date,
                            cancellationToken),

            // قيم مؤقتة
            AverageAiResponseTime = 0,

            ErrorRate = 0
        };
    }
}