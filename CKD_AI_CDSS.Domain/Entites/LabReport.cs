using CKD_AI_CDSS.Domain.Common;
using CKD_AI_CDSS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class LabReport : BaseEntity
    {
        public int PatientId { get; set; }

        public decimal? Creatinine { get; set; }

        public decimal? Egfr { get; set; }

        public decimal? Urea { get; set; }

        public string? BloodPressure { get; set; }

        public decimal? Glucose { get; set; }

        public decimal? Hemoglobin { get; set; }

        public decimal? Albumin { get; set; }

        public decimal? Sodium { get; set; }

        public decimal? Potassium { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public ProcessingStatus Status { get; set; }

        public Patient Patient { get; set; } = null!;
    }
}
