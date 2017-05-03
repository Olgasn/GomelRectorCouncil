using System.ComponentModel.DataAnnotations;

namespace GomelRectorCouncil.Models
{
    // Достижения
    public class Achievement
    {
        [Key]
        public int AchievementId {get; set;}

        public int IndicatorId {get; set;}

        public int UnivercityId {get; set;}

        public float IndicatorValue {get; set;}

        public University Univercity {get; set;}
        
        public Indicator Indicator {get; set;}
    }
}
