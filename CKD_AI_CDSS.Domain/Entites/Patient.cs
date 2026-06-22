using CKD_AI_CDSS.Domain.Common;
using CKD_AI_CDSS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class Patient : BaseEntity
    {
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }

        public Gender? Gender { get; set; }

        public decimal? Weight { get; set; }

        public decimal? Height { get; set; }

        public decimal? BMI { get; set; }

        public string? MedicalHistory { get; set; }

        public string? Symptoms { get; set; }

        public string? LifestyleInfo { get; set; }

        public User User { get; set; } = null!;

        public ICollection<LabReport> LabReports { get; set; }
            = new List<LabReport>();

        public ICollection<RetinalImage> RetinalImages { get; set; }
            = new List<RetinalImage>();

        public ICollection<Prediction> Predictions { get; set; }
            = new List<Prediction>();

        public ICollection<MonitoringHistory> MonitoringHistories { get; set; }
            = new List<MonitoringHistory>();

        public ICollection<ChatbotSession> ChatbotSessions { get; set; }
            = new List<ChatbotSession>();
    }
}
