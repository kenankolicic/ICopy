using iCopy.ExternalServices.Mail;
using iCopy.ExternalServices.Model;
using iCopy.SERVICES.Attributes;
using iCopy.SERVICES.IServices;
using iCopy.Web.Options;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using iCopy.Web.Helper;
using Microsoft.AspNetCore.DataProtection;
using Org.BouncyCastle.Crypto.Signers;

namespace iCopy.Web.Areas.Auth.Controllers
{
    [Area(Helper.Strings.Area.Auth)]
    public class SignUpController : Controller
    {
        private readonly IClientService ClientService;
        private readonly SharedResource _localizer;
        private readonly IMailService MailService;
        private readonly EmailServerNoReplyOptions ServerNoReplyOptions;
        private readonly IUserService UserService;
        private readonly Constants Constants;
        private readonly IDataProtector protector;

        public SignUpController(IClientService ClientService, 
            SharedResource _localizer, 
            IMailService MailService, 
            EmailServerNoReplyOptions ServerNoReplyOptions, 
            IUserService UserService,
            Constants Constants,
            IDataProtectionProvider protectionProvider)
        {
            this.ClientService = ClientService;
            this._localizer = _localizer;
            this.MailService = MailService;
            this.ServerNoReplyOptions = ServerNoReplyOptions;
            this.UserService = UserService;
            this.Constants = Constants;
            this.protector = protectionProvider.CreateProtector(Strings.Protection.UrlEncription);
        }

        [HttpGet]
        [LoggedInRedirectToAction(RedirectToAction = Settings.Routes.Dashboard.Index)]
        public Task<ViewResult> Index() => Task.FromResult(View(new Model.Request.Client()));

        [HttpPost, Transaction, AutoValidateModelState]
        public async Task<IActionResult> Index([FromForm] Model.Request.Client client)
        {
            try
            {
                Model.Response.Client addedClient = await ClientService.InsertAsync(client);
                addedClient = await ClientService.GetByIdAsync(addedClient.ID);
                var token = await UserService.GenerateAccountActivationToken(addedClient.ApplicationUserId);
                var encryptedToken = System.Web.HttpUtility.UrlEncode(protector.Protect(Encoding.UTF8.GetBytes(token)));
                var encriptedUser = System.Web.HttpUtility.UrlEncode(protector.Protect(Encoding.UTF8.GetBytes(addedClient.ApplicationUserId.ToString())));
                await MailService.SendMailAsync(new MailMessage
                {
                    To = addedClient.ApplicationUser.Email,
                    Subject = Constants.EmailActivationAccountSubject(),
                    MailServer = new MailServer
                    {
                        Email = ServerNoReplyOptions.Email,
                        Name = ServerNoReplyOptions.Name,
                        Password = ServerNoReplyOptions.Password,
                        Username = ServerNoReplyOptions.Username,
                        Port = ServerNoReplyOptions.Port,
                        Url = ServerNoReplyOptions.Domain
                    },
                    Body = Constants.EmailActivationAccountBody($"{HttpContext.Request.Scheme}:/{HttpContext.Request.Host}{Settings.Routes.Login.ActivateAcount}?user={encriptedUser}&token={encryptedToken}"),
                });
                return Json(new {success = true, message = _localizer.SuccRegister});
            }
            catch (Exception e)
            {
                return StatusCode((int) HttpStatusCode.InternalServerError);
            }
        }
    }
}
