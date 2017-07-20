using System.Threading.Tasks;

namespace GomelRectorCouncil.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
