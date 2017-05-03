﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GomelRectorCouncil.Models
{
    public class Rector
    {
        [Key]
        public int RectorId { get; set; }

        [StringLength(50)]
        [Required (ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Имя не может быть длиннее чем 50 символов.")]
        [Display(Name = "Имя")]
        public string FirstMidName { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Отчестыво не может быть длиннее чем 60 символов.")]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Display(Name = "Полное имя")]
        public string FullName
        {
            get
            {
                return LastName + " " + FirstMidName+ " " + MiddleName;
            }
        }
        
        [EmailAddress (ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        public int UniversityId {get; set;}
        public University University { get; set; }
    }
}
