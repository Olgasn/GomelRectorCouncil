using System.ComponentModel.DataAnnotations;

namespace GomelRectorCouncil.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
