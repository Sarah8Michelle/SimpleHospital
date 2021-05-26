using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleHospital.Models
{
    public class Collaborator
    {
        public int CollaboratorId { get; set; }

        [Display(Name = "Collaborator")]
        public int PersonId { get; set; }

        [Display(Name = "Type of Collaborator")]
        public int CollaboratorTypeId { get; set; }

        public bool Deleted { get; set; }

        [ForeignKey("PersonId")]
        public virtual Person Person { get; set; }

        [ForeignKey("CollaboratorTypeId")]
        public virtual CollaboratorType CollaboratorType { get; set; }
    }
}
