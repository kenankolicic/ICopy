using AutoMapper;
using iCopy.Model.Request;
using iCopy.SERVICES.Attributes;
using iCopy.SERVICES.IServices;
using iCopy.Web.Controllers;
using iCopy.Web.Helper;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace iCopy.Web.Areas.Administration.Controllers
{
    [Area(Strings.Area.Administration), AllowAnonymous()]
    [Authorize(Roles = Strings.Roles.AdministratorCompany)]
    public class CopierController : BaseDataTableCRUDController<Model.Request.Copier, Model.Request.Copier, Model.Response.Copier, Model.Request.CopierSearch, int>
    {
        private new readonly ICopierService crudService;

        private Model.Request.ProfilePhoto PhotoSession => HttpContext.Session.Get<Model.Request.ProfilePhoto>(Session.Keys.Upload.ProfileImage);

        public CopierController(ICopierService CrudService, SharedResource _localizer, IMapper mapper) : base(CrudService, _localizer, mapper)
        {
            this.crudService = CrudService;
        }

        [HttpPost, Transaction, AutoValidateModelState, Authorize(Roles = Strings.Roles.Administrator)]
        public override async Task<IActionResult> Insert(Copier model)
        {
            try
            {
                model.ProfilePhoto = PhotoSession;
                await crudService.InsertAsync(model);
                TempData["success"] = _localizer.SuccAdd;
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
        public override async Task<IActionResult> Update(int id, [FromForm]Copier model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ProfilePhoto = PhotoSession;
                    await crudService.UpdateAsync(id, model);
                    TempData["success"] = _localizer.SuccUpdate;
                    return RedirectToAction(nameof(Update));
                }
                catch
                {
                    TempData["error"] = _localizer.ErrUpdate;
                }
            }

            return View(await crudService.GetByIdAsync(id));
        }

        [HttpGet, Transaction, Authorize(Roles = Strings.Roles.Administrator)]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpPost, IgnoreAntiforgeryToken, Authorize(Roles = Strings.Roles.Administrator)]
        public override Task<IActionResult> ChangeActiveStatus(int id)
        {
            return base.ChangeActiveStatus(id);
        }
    }
}
