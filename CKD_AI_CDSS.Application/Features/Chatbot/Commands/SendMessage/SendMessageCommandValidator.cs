using FluentValidation;

namespace CKD_AI_CDSS.Application.Features.Chatbot.Commands.SendMessage;

public class SendMessageCommandValidator
    : AbstractValidator<SendMessageCommand>
{
    public SendMessageCommandValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .MaximumLength(1000);
    }
}