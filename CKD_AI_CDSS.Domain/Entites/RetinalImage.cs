using CKD_AI_CDSS.Domain.Common;
using CKD_AI_CDSS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class RetinalImage : BaseEntity
    {
        public int PatientId { get; set; }

        public int UploadedBy { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public ProcessingStatus Status { get; set; }

        public Patient Patient { get; set; } = null!;

        public User UploadedDoctor { get; set; } = null!;

        public RetinalAnalysis? RetinalAnalysis { get; set; }
    }
}
