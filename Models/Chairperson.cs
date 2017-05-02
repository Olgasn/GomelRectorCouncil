using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    public class Chairperson
    {
        [Key]
        public int ChairpersonId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        [Display(Name="Дата назначения")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Display(Name="Дата отставки")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? StopDate { get; set; }
        public int RectorID { get; set; }
        public Rector Rector { get; set; }
        public ICollection<Document> Documents { get; set; }

    }
}
