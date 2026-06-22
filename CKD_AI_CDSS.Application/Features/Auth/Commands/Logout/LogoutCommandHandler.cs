using MediatR;

namespace CKD_AI_CDSS.Application.Features.Auth.Commands.Logout;

public class LogoutCommandHandler
    : IRequestHandler<LogoutCommand, bool>
{
    public async Task<bool> Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        return true;
    }
}