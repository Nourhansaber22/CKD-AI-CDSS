using CKD_AI_CDSS.Domain.Common;
using CKD_AI_CDSS.Domain.Enums;
namespace CKD_AI_CDSS.Domain.Entites
{
    public class Prediction : BaseEntity
    {
        public int PatientId { get; set; }

        public int LabReportId { get; set; }

        public int? RetinalAnalysisId { get; set; }

        public RiskLevel RiskLevel { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public decimal ProbabilityScore { get; set; }

        public string? ProgressionEstimate { get; set; }

        public string? ShapValues { get; set; }

        public int? ReviewedBy { get; set; }

        public DateTime? ReviewTimestamp { get; set; }

        public Patient Patient { get; set; } = null!;

        public LabReport LabReport { get; set; } = null!;

        public RetinalAnalysis? RetinalAnalysis { get; set; }

        public Doctor? ReviewedByDoctor { get; set; }

        public MonitoringHistory? MonitoringHistory { get; set; }
        public ICollection<MonitoringHistory> MonitoringHistories { get; set; }
                   = new List<MonitoringHistory>();
    }
}
