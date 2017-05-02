using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Models
{
    public class Document
    {
        [Key]
        [Display(Name="Код документа")]
        public int DocumentId {get; set;}

        [Display(Name="Регистрационный номер")]
        public string RegistrationNumber {get; set}

        [Display(Name="Название")]
        public string DocumentName {get; set;}

        [Display(Name="Содержание")]
        public string DocumentDescription {get; set;}

        [Display(Name="Дата регистрации")]
        [DateType(DateType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate {get; set}

        [Display(Name="Файл")]
        public string DocumentURL {get; set;}

        public int ChairpersonId { get; set; }
        public Chairperson Chairperson {get; set;}
    }
}
