using MediatR;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Logout;

public record LogoutCommand : IRequest<bool>;