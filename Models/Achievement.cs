using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    public class Achievement
    {
        [Key]
        public int AchievementId {get; set;}
        public int IndicatorId {get; set;}
        public int UnivercityId {get; set;}
        public University Univercity {get; set;}
        public Indicator Indicator {get; set;}
    }
}
