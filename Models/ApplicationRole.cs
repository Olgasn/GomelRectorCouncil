using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace GomelRectorCouncil.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationRole : IdentityRole
    {
        [Display(Name = "Университет")]
        public int UniversityId { get; set; }

    }
}
