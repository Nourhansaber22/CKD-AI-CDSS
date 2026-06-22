using CKD_AI_CDSS.Application.Features.Auth.Responses;
using CKD_AI_CDSS.Domain.Enums;
using MediatR;
namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public string? Specialization { get; set; }

        public string? ClinicName { get; set; }
    }
}
