using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    public enum IndicatorType
        {
        text, int_number, float_number
        }
    public class Indicator
    {
        
        [Key]
        public int IndicatorId {get; set;}

        public int IndicatorId1 {get; set;}
        public int IndicatorId2 {get; set;}
        public int IndicatorId3 {get; set;}

        public int IndicatorValue {get; set;}
        
        public string IndicatorName {get; set;}

        [DisplayFormat(NullDisplayText = "-")]
        public IndicatorType? IndicatorType { get; set; }

        public string IndicatorDescription {get; set;}

        [Required]
        [Range(2010, 2050, ErrorMessage = "Недопустимый год")]
        public int Year {get; set;}


        public ICollection<Achievement> Achievements {get; set;}

    }
}
