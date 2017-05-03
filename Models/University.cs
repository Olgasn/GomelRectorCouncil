using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    // Университет
    public class University
    {
        [Key]
        [Display(Name = "Код")]
        public int UniversityId {get; set;}
        
        [Display(Name = "Университет")]
        public string UniversityName {get; set;}

        public int RectorId {get; set;}

        public Rector Rector {get; set;}

        public ICollection<Achievement> Achievements {get; set;}

    }
}
