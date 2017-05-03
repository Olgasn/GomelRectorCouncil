﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    // Нормативные документы
    public class Document
    {
        [Key]
        [Display(Name="Код документа")]
        public int DocumentId {get; set;}

        [Display(Name="Регистрационный номер")]
        public string RegistrationNumber {get; set;}

        [Display(Name="Название")]
        public string DocumentName {get; set;}

        [Display(Name="Содержание")]
        public string DocumentDescription {get; set;}

        [Display(Name="Дата регистрации")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate {get; set;}

        [Display(Name="Файл")]
        public string DocumentURL {get; set;}

        public int ChairpersonId { get; set; }
        public Chairperson Chairperson {get; set;}
    }
}
