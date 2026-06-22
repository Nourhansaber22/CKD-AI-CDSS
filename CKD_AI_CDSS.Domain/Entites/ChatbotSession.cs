using CKD_AI_CDSS.Domain.Common;
namespace CKD_AI_CDSS.Domain.Entites
{
    public class ChatbotSession : BaseEntity
    {
        public int PatientId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Message { get; set; } = string.Empty;

        public string Response { get; set; } = string.Empty;

        public Patient Patient { get; set; } = null!;
    }
}
