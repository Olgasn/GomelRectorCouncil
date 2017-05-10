using GomelRectorCouncil.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GomelRectorCouncil.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        
        public ICollection<ApplicationUser> Users { get; }
        public ICollection<University> Universities { get; }
               

    }
}
