using AutoMapper;
using iCopy.SERVICES.Attributes;
using iCopy.SERVICES.Exceptions;
using iCopy.SERVICES.IServices;
using iCopy.Web.Helper;
using iCopy.Web.Models;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace iCopy.Web.Areas.Administration.Controllers
{
    [Area(Strings.Area.Administration)]
    [Authorize(Roles = Strings.Roles.Administrator)]
    public class UserController : Controller
    {
        private readonly SharedResource _localizer;
        private readonly IUserService CrudService;
        private readonly ValidationErrors _validationErrors;

        public UserController(IUserService CrudService, SharedResource _localizer, IMapper mapper, ValidationErrors _validationErrors)
        {
            this.CrudService = CrudService;
            this._localizer = _localizer;
            this._validationErrors = _validationErrors;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int numberOfRecords = await CrudService.GetNumberOfRecordsAsync();
            DataTable<Model.Response.ApplicationUser> model = new DataTable<Model.Response.ApplicationUser>()
            {
                recordsFiltered = numberOfRecords,
                recordsTotal = numberOfRecords,
                data = await CrudService.TakeRecordsByNumberAsync()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            Model.Response.ApplicationUser model = await CrudService.GetByIdAsync(id);
            if (model != null)
                return View(model);
            return NotFound();
        }

        [HttpPost, Transaction, AutoValidateModelState]
        public async Task<IActionResult> UpdatePassword(int id, [FromForm] Model.Request.ChangePassword model)
        {
            try
            {
                await CrudService.UpdatePassword(id, model);
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

        [HttpPost, Transaction, AutoValidateModelState]
        public async Task<IActionResult> Update(int id, Model.Request.ApplicationUserUpdate model)
        {
            try
            {
                await CrudService.UpdateAsync(id, model);
                return Json(new {success = true, message = _localizer.SuccUserUpdate});
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

        [HttpPost, IgnoreAntiforgeryToken]
        public async Task<DataTable<Model.Response.ApplicationUser>> GetData([FromForm]Model.Request.ApplicationUserSearch Search, [FromForm]DataTableRequest Request)
        {
            var filteredData = await CrudService.GetByParametersAsync(Search, Request.order[0].dir, Request.columns[Request.order[0].column].name, Request.start, Request.length);
            return new DataTable<Model.Response.ApplicationUser>
            {
                draw = Request.draw,
                recordsTotal = filteredData.Item2,
                recordsFiltered = filteredData.Item2,
                data = filteredData.Item1
            };
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) => View(await CrudService.GetByIdAsync(id));

        [HttpPost, IgnoreAntiforgeryToken]
        public virtual async Task<IActionResult> ChangeActiveStatus(int id)
        {
            try
            {
                await CrudService.ChangeActiveStatusAsync(id);
                return Json(new { success = true, message = _localizer.SuccChangeStatus });
            }
            catch
            {
                return Json(new { success = false, message = _localizer.ErrChangeStatus });
            }
        }
    }
}
