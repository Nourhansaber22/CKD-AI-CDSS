using CKD_AI_CDSS.Application.Interfaces;
using CKD_AI_CDSS.Domain.Entites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CKD_AI_CDSS.Application.Features.Chatbot.Commands.SendMessage;

public class SendMessageCommandHandler
    : IRequestHandler<SendMessageCommand, ChatbotResponse>
{
    private readonly IApplicationDbContext _context;

    public SendMessageCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ChatbotResponse> Handle(
        SendMessageCommand request,
        CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .FirstOrDefaultAsync(
                x => x.UserId == request.UserId,
                cancellationToken);

        if (patient is null)
            throw new Exception("Patient not found");

        var responseText =
            $"You said: {request.Message}";

        var session = new ChatbotSession
        {
            PatientId = patient.Id,
            Message = request.Message,
            Response = responseText
        };

        _context.ChatbotSessions.Add(session);

        await _context.SaveChangesAsync(
            cancellationToken);

        return new ChatbotResponse
        {
            Response = responseText,
            Type = "General"
        };
    }
}