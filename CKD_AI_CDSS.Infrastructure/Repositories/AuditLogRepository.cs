using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Infrastructure.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog entity)
            => await _context.AuditLogs.AddAsync(entity);

        public async Task<List<AuditLog>> GetAllAsync()
            => await _context.AuditLogs
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        public async Task<List<AuditLog>> GetAuditLogsAsync(CancellationToken cancellationToken)
        {
            return await _context.AuditLogs
                .ToListAsync(cancellationToken);
        }
    }
}
