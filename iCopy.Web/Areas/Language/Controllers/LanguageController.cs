using iCopy.Web.Helper;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace iCopy.Web.Areas.Language.Controllers
{
    [Area(Strings.Area.Language)]
    [Authorize]
    public class LanguageController : Controller
    {
        public IActionResult Change(string culture)
        {
            if (!string.IsNullOrWhiteSpace(culture) && Localization.SupportedCultures.Any(x => x.Name == culture))
            {
                CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                Response.Cookies.SetCultureInfoCookie(new RequestCulture(culture));
            }

            if (Request.Headers["Referer"] != StringValues.Empty)
                return Redirect(Request.Headers["Referer"]);
            return LocalRedirect("~/");
        }
    }
}