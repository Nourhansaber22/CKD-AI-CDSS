using MediatR;

namespace CKD_AI_CDSS.Application.Features.Chatbot.Commands.SendMessage;

public record SendMessageCommand(
    int UserId,
    string Message)
    : IRequest<ChatbotResponse>;