using AutoMapper;
using iCopy.Model.Request;
using iCopy.SERVICES.Attributes;
using iCopy.SERVICES.IServices;
using iCopy.Web.Controllers;
using iCopy.Web.Helper;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace iCopy.Web.Areas.Administration.Controllers
{

    [Area(Strings.Area.Administration), Authorize(Roles = Strings.Roles.Administrator)]
    [Authorize(Roles = Strings.Roles.Administrator)]
    public class CompanyController : BaseDataTableCRUDController<Model.Request.Company, Model.Request.Company, Model.Response.Company, Model.Request.CompanySearch, int>
    {
        private readonly ICompanyService CrudService;

        private Model.Request.ProfilePhoto PhotoSession => HttpContext.Session.Get<Model.Request.ProfilePhoto>(Session.Keys.Upload.ProfileImage);

        public CompanyController(ICompanyService CrudService, SharedResource _localizer, IMapper mapper) : base(CrudService, _localizer, mapper)
        {
            this.CrudService = CrudService;
        }

        [HttpPost, Transaction, AutoValidateModelState]
        public override async Task<IActionResult> Insert(Company model)
        {
            try
            {
                model.ProfilePhoto = PhotoSession;
                await CrudService.InsertAsync(model);
                TempData["success"] = _localizer.SuccAdd;
                HttpContext.Session.Remove(Session.Keys.Upload.ProfileImage);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public override Task<IActionResult> Update(int id)
        {
            if (HttpContext.Session.Get(Session.Keys.Upload.ProfileImage) != null)
                HttpContext.Session.Remove(Session.Keys.Upload.ProfileImage);
            return base.Update(id);
        }

        [HttpPost, Transaction]
        public override async Task<IActionResult> Update(int id, [FromForm]Company model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ProfilePhoto = PhotoSession;
                    await CrudService.UpdateAsync(id, model);
                    TempData["success"] = _localizer.SuccUpdate;
                    return RedirectToAction(nameof(Update));

                }
                catch
                {
                    TempData["error"] = _localizer.ErrUpdate;
                }
            }

            return View(await CrudService.GetByIdAsync(id));
        }
    }
}
