using System.ComponentModel.DataAnnotations;

namespace GomelRectorCouncil.Models
{
    // Фактическое достижение университета по заданному показателю в заданном году
    public class Achievement
    {
        [Key]
        public int AchievementId {get; set;}

        public int IndicatorId {get; set;}

        public int UnivercityId {get; set;}

        [Display(Name="Значение показателя")]
        public float IndicatorValue {get; set;}

        public University Univercity {get; set;}
        
        public Indicator Indicator {get; set;}
    }
}
