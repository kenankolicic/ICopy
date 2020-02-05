using iCopy.SERVICES.Attributes;
using iCopy.SERVICES.Exceptions;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using iCopy.Web.Helper;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace iCopy.Web.Areas.User.Controllers
{
    [Area(Strings.Area.User)]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserService UserService;
        private readonly SharedResource _localizer;
        private readonly ValidationErrors _validationErrors;

        public ProfileController(IUserService UserService, SharedResource localizer, ValidationErrors validationErrors)
        {
            this.UserService = UserService;
            this._localizer = localizer;
            this._validationErrors = validationErrors;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await UserService.GetByIdAsync(User.GetUserId());
            return View(user);
        }

        [HttpPost, Transaction, AutoValidateModelState]
        public async Task<IActionResult> Update(Model.Request.ApplicationUserUpdate model)
        {
            try
            {
                await UserService.UpdateAsync(User.GetUserId(), model);
                return Json(new { success = true, message = _localizer.SuccUserUpdate });
            }
            catch (ModelStateException e)
            {
                ModelState.AddModelError(e.Key, _validationErrors.LocalizedString(e.Message));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", _localizer.ErrUserUpdate);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(x => x.Key, x => x.Value.Errors.Select(y => y.ErrorMessage)));
        }

        [HttpPost, Transaction, AutoValidateModelState]
        public async Task<IActionResult> UpdatePassword([FromForm] Model.Request.ChangePassword model)
        {
            try
            {
                await UserService.UpdatePassword(User.GetUserId(), model);
                return Json(new { success = true, message = _localizer.SuccPasswordUpdated });
            }
            catch (ModelStateException e)
            {
                ModelState.AddModelError(e.Key, _localizer.LocalizedString(e.Message));
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", _localizer.ErrUpdatePassword);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(ModelState.Where(x => x.Value.Errors.Count > 0).ToDictionary(x => x.Key, x => x.Value.Errors.Select(y => y.ErrorMessage)));
        }
    }
}
