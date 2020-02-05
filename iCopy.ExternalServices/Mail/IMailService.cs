using System.Threading.Tasks;
using iCopy.ExternalServices.Model;

namespace iCopy.ExternalServices.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(MailMessage message);
    }
}
