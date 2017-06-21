using System.Threading.Tasks;

namespace GomelRectorCouncil.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
