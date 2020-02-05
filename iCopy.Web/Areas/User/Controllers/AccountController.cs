using AutoMapper;
using iCopy.SERVICES.IServices;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iCopy.Web.Areas.User.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService crudService;
        private readonly SharedResource _localizer;
        public AccountController(IUserService crudService, SharedResource _localizer, IMapper mapper) 
        {
            this.crudService = crudService;
            this._localizer = _localizer;
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(int id, [FromForm] Model.Request.ChangePassword model, string redirectUrl)
        {
            if (ModelState.IsValid)
            {
                await crudService.UpdatePassword(id, model);
                TempData["success"] = _localizer.SuccPasswordUpdated;
            }

            return LocalRedirect(redirectUrl);
        }
    }
}
