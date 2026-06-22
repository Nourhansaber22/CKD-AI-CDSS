using CKD_AI_CDSS.Application.Features.Auth.Responses;
using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler
    : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public LoginCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<AuthResponse> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Email == request.Email,
                cancellationToken);

        if (user is null)
            throw new Exception("Invalid email or password");

        if (!user.IsActive)
            throw new Exception("Account is inactive");

        var isPasswordValid =
            _passwordHasher.VerifyPassword(
                request.Password,
                user.PasswordHash);

        if (!isPasswordValid)
            throw new Exception("Invalid email or password");

        var jwtToken =
            _jwtTokenGenerator.GenerateToken(user);

        var refreshTokenValue =
            _refreshTokenGenerator.Generate();

        var refreshToken = new CKD_AI_CDSS.Domain.Entites.RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };

        await _context.RefreshTokens.AddAsync(
            refreshToken,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        return new AuthResponse
        {
            UserId = user.Id,
            Token = jwtToken,
            RefreshToken = refreshTokenValue,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString()
        };
    }
}