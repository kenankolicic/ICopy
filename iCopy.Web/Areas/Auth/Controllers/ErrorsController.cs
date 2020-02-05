using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace iCopy.Web.Areas.Auth.Controllers
{
    [Area(Helper.Strings.Area.Auth), AllowAnonymous]
    public class ErrorsController : Controller
    {
        public Task<IActionResult> Error401(string returnUrl) => Task.FromResult<IActionResult>(View("401"));
    }
}
