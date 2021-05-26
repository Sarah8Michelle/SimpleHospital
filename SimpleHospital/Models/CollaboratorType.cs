using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospital.Models
{
    public class CollaboratorType
    {
        public int CollaboratorTypeId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Types of Collaborators")]
        public string CollaboratorTypes { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Department { get; set; }

        public bool Deleted { get; set; }
    }
}
