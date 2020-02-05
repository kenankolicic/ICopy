using AutoMapper;
using iCopy.Model.Request;
using iCopy.Model.Response;
using iCopy.SERVICES.Attributes;
using iCopy.SERVICES.Extensions;
using iCopy.SERVICES.IServices;
using iCopy.Web.Controllers;
using iCopy.Web.Helper;
using iCopy.Web.Models;
using iCopy.Web.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace iCopy.Web.Areas.Administration.Controllers
{
    [Area(Strings.Area.Administration)]
    [Authorize]
    public class PrintRequestController : BaseDataTableCRUDController<Model.Request.PrintRequest, Model.Request.PrintRequest, Model.Response.PrintRequest, Model.Request.PrintRequestSearch, int>
    {
        private new readonly IPrintRequestService crudService;
        private Model.Request.PrintRequestFile PrintRequestFile => HttpContext.Session.Get<Model.Request.PrintRequestFile>(Session.Keys.Upload.PrintRequestFile);

        public PrintRequestController(IPrintRequestService CrudService, SharedResource _localizer, IMapper mapper) : base(CrudService, _localizer, mapper)
        {
            this.crudService = CrudService;
        }

        [HttpPost, Transaction, AutoValidateModelState]
        public override async Task<IActionResult> Insert(Model.Request.PrintRequest model)
        {
            try
            {
                model.FilePath = PrintRequestFile.Path;
                model.ClientId = User.GetId();

                await crudService.InsertAsync(model);
                
                TempData["success"] = _localizer.SuccAdd;
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost, Transaction]
        public override async Task<IActionResult> Update(int id, [FromForm]Model.Request.PrintRequest model)
        {
            if (ModelState.IsValid)
            {
                try
                {
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

        [HttpGet, Transaction]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }

        [HttpPost, IgnoreAntiforgeryToken]
        public override Task<IActionResult> ChangeActiveStatus(int id)
        {
            return base.ChangeActiveStatus(id);
        }
    }
}