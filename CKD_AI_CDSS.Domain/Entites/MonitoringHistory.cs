using CKD_AI_CDSS.Domain.Common;
using CKD_AI_CDSS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class MonitoringHistory : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int PredictionId { get; set; }
        public Prediction Prediction { get; set; }
        public RiskLevel RiskLevel { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;

        public RiskLevel RiskLevelSnapshot { get; set; }

        public TrendIndicator TrendIndicator { get; set; }
    }
}
