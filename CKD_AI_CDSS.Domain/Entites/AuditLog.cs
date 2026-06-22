using CKD_AI_CDSS.Domain.Common;
namespace CKD_AI_CDSS.Domain.Entites
{
    public class AuditLog : BaseEntity
    {
        public int? UserId { get; set; }

        public string Action { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }

        public User? User { get; set; }
    }
}
