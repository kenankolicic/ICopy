using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Attributes
{
    public class LoggedInRedirectToActionAttribute : ActionFilterAttribute
    {
        public string RedirectToAction { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
                context.HttpContext.Response.Redirect(RedirectToAction);
            else
            {
                await next();
            }
        }
    }
}
