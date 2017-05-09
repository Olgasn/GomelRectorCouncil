using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace GomelRectorCouncil.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Year { get; set; }
        public int UniversityId { get; set; }
        public University University { get; set; }  // команда игрока

    }
}
