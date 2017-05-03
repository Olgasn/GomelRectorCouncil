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

        [Required]
        [Range(1, 100, ErrorMessage = "Недопустимое значение кода")]
        public byte IndicatorId1 {get; set;}
        public byte? IndicatorId2 {get; set;}
        public byte? IndicatorId3 {get; set;}


        
        public string IndicatorName {get; set;}

        [DisplayFormat(NullDisplayText = "-")]
        public IndicatorType? IndicatorType { get; set; }

        public string IndicatorDescription {get; set;}

        [Required]
        [Range(2010, 2050, ErrorMessage = "Недопустимый год")]
        public int Year {get; set;}

        public string IndicatorCode {
            get
            {
                return Convert.ToString(IndicatorId1) +"."+ Convert.ToString(IndicatorId2) +"."+ Convert.ToString(IndicatorId3);

            }
        }
        public ICollection<Achievement> Achievements {get; set;}

    }
}
