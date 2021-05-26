using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleHospital.Models
{
    public class Pacient
    {
        public int PacientId { get; set; }

        public int PersonId { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }
    }
}
