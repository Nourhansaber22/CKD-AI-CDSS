using CKD_AI_CDSS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class RetinalAnalysis : BaseEntity
    {
        public int RetinalImageId { get; set; }

        public string Features { get; set; } = string.Empty;

        public string? FeatureImportance { get; set; }

        public string? HeatmapPath { get; set; }

        public RetinalImage RetinalImage { get; set; } = null!;
    }
}
