using CKD_AI_CDSS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class Doctor : BaseEntity
    {
        public int UserId { get; set; }

        public string? Specialization { get; set; }

        public string? ClinicName { get; set; }

        public User User { get; set; } = null!;

        public ICollection<Prediction> ReviewedPredictions { get; set; }
            = new List<Prediction>();
    }
}
