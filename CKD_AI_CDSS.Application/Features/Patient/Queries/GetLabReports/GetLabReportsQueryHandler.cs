using CKD_AI_CDSS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Patient.Queries.GetLabReports;

public class GetLabReportsQueryHandler
    : IRequestHandler<GetLabReportsQuery,
        List<LabReportResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetLabReportsQueryHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LabReportResponse>> Handle(
        GetLabReportsQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.LabReports
            .Where(x => x.PatientId == request.PatientId)
            .Select(x => new LabReportResponse
            {
                Id = x.Id,
                CreatedAt = x.CreatedAt,
                FilePath = x.FilePath,
                Status = x.Status
            })
            .ToListAsync(cancellationToken);
    }
}