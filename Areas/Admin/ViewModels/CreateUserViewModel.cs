using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class CreateUserViewModel
    {
        [Display(Name="Имя пользователя")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Дата регистрации")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Университет")]
        public int UniversityId { get; set; }
    }

}
