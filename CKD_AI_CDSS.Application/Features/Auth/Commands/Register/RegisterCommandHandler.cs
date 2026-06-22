using CKD_AI_CDSS.Application.Features.Auth.Responses;
using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using CKD_AI_CDSS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public RegisterCommandHandler(
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
        RegisterCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _context.Users
            .AnyAsync(
                x => x.Email == request.Email,
                cancellationToken);

        if (exists)
            throw new Exception("Email already exists");

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash =
                _passwordHasher.HashPassword(request.Password),
            Role = request.Role
        };

        await _context.Users.AddAsync(
            user,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        if (request.Role == UserRole.Patient)
        {
            await _context.Patients.AddAsync(
                new CKD_AI_CDSS.Domain.Entites.Patient
                {
                    UserId = user.Id
                },
                cancellationToken);
        }

        if (request.Role == UserRole.Doctor)
        {
            await _context.Doctors.AddAsync(
                new CKD_AI_CDSS.Domain.Entites.Doctor
                {
                    UserId = user.Id,
                    Specialization = request.Specialization,
                    ClinicName = request.ClinicName
                },
                cancellationToken);
        }

        await _context.SaveChangesAsync(
            cancellationToken);

        var jwtToken =
            _jwtTokenGenerator.GenerateToken(user);

        var refreshTokenValue =
            _refreshTokenGenerator.Generate();

        await _context.RefreshTokens.AddAsync(
            new CKD_AI_CDSS.Domain.Entites.RefreshToken
            {
                Token = refreshTokenValue,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false
            },
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