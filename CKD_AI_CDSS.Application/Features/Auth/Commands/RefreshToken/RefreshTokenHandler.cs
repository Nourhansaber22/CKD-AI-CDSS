using CKD_AI_CDSS.Application.Features.Auth.Responses;
using CKD_AI_CDSS.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtTokenGenerator _jwt;

    public RefreshTokenHandler(IApplicationDbContext context, IJwtTokenGenerator jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // ⚠️ POC فقط
        // هنفترض التوكن valid ونجيب userId منه في الحقيقي

        throw new NotImplementedException("Refresh token logic will be upgraded later");
    }
}