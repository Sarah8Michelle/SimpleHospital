using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospital.Models
{
    public class MedicalHistory
    {
        public int MedicalHistoryId { get; set; }

        [Required]
        public int PacientId { get; set; }

        [Required]
        public string Symptoms { get; set; }

        [Required]
        public string Allergies { get; set; }

        [Required]
        public string Diseases { get; set; }

        [Required]
        public bool Surgeries { get; set; }

        [Required]
        [Display(Name = "Family Records")]
        public string FamilyHistory { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey("PacientId")]
        public virtual Pacient Pacient { get; set; }
    }
}
