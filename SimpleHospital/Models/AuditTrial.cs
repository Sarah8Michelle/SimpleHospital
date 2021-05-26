using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospital.Models
{
    public class AuditTrial
    {
        public int Id { get; set; }

        public int KeyFieldId { get; set; }

        public int AuditActionTypeENUM { get; set; }

        public DateTime DateTimeStamp { get; set; }

        public string Changes { get; set; }

        public string ValueBefore { get; set; }

        public string ValueAfter { get; set; }
    }
}
