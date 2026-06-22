using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Repositories;

public class RetinalImageRepository : IRetinalImageRepository
{
    private readonly ApplicationDbContext _context;

    public RetinalImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> UploadImageAsync(RetinalImage image, CancellationToken cancellationToken)
    {
        await _context.RetinalImages.AddAsync(image, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return image.Id;
    }

    public async Task<RetinalImage?> GetByIdAsync(int id)
        => await _context.RetinalImages.FindAsync(id);

    public async Task<List<RetinalImage>> GetByPatientIdAsync(int patientId)
        => await _context.RetinalImages
            .Where(x => x.PatientId == patientId)
            .ToListAsync();

    public async Task AddAsync(RetinalImage entity)
        => await _context.RetinalImages.AddAsync(entity);
}