using CKD_AI_CDSS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CKD_AI_CDSS.Domain.Entites
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; }

        public User User { get; set; } = null!;
    }
}
