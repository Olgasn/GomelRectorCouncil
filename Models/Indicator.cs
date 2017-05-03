using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    public class Indicator
    {
        [Key]
        public int IndicatorId {get; set;}
        public string IndicatorName {get; set;}
        public string IndicatorDescription {get; set;}
        
        [Required]
        [Range(2010, 2050, ErrorMessage = "Недопустимый год")]
        public int Year {get; set;}

        public ICollection<Achievement> Achievements {get; set;}

    }
}
