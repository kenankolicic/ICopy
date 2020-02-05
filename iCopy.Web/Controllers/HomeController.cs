using iCopy.ExternalServices.Mail;
using iCopy.ExternalServices.Model;
using iCopy.Web.Models;
using iCopy.Web.Options;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace iCopy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService mailService;
        private readonly EmailServerNoReplyOptions ServerNoReplyOptions;
        private readonly IDataProtector protector;

        public HomeController(IMailService mailService, EmailServerNoReplyOptions ServerNoReplyOptions, IDataProtectionProvider p)
        {
            this.mailService = mailService;
            this.ServerNoReplyOptions = ServerNoReplyOptions;
            this.protector = p.CreateProtector("SessionMiddleware");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public async Task<string> SendEmail()
        {
            await mailService.SendMailAsync(new MailMessage
            {
                Body = "Ovo je tijelo mejla",
                Subject = "Neki naslov",
                To = "emirveledar5@gmail.com",
                MailServer = new MailServer()
                {
                    Url = ServerNoReplyOptions.Domain,
                    Name = ServerNoReplyOptions.Name,
                    Email = ServerNoReplyOptions.Email,
                    Username = ServerNoReplyOptions.Username,
                    Password = ServerNoReplyOptions.Password,
                    Port = ServerNoReplyOptions.Port
                }
            });
            return "Dobar";
        }
    }
}
