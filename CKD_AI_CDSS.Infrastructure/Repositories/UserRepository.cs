using CKD_AI_CDSS.Application.Features.Admin.Commands.UpdateUser;
using CKD_AI_CDSS.Application.Features.Admin.Queries.GetUsers;
using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserResponse>> GetUsersAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .Select(u => new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString(),
                IsActive = u.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateUserAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id == command.UserId,
                cancellationToken);

        if (user is null)
            return false;

        user.Name = command.Name;
        user.Email = command.Email;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> ActivateUserAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id == userId,
                cancellationToken);

        if (user is null)
            return false;

        user.IsActive = true;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeactivateUserAsync(
        int userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id == userId,
                cancellationToken);

        if (user is null)
            return false;

        user.IsActive = false;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}