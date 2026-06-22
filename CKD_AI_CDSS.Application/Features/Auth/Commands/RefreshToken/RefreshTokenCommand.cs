using CKD_AI_CDSS.Application.Features.Auth.Responses;
using MediatR;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(string Token) : IRequest<AuthResponse>;