using CKD_AI_CDSS.Application.Features.Auth.Responses;
using MediatR;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; } = default!;

    public string Password { get; set; } = default!;
}