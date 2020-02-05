using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace iCopy.Web.Helper
{
    public static class Cookie
    {
        public static void SetCultureInfoCookie(this IResponseCookies cookies, RequestCulture culture)
        {
            cookies.Delete(Localization.CurrentCultureCookieName);
            cookies.Append(Localization.CurrentCultureCookieName, CookieRequestCultureProvider.MakeCookieValue(culture));
        }
    }
}
