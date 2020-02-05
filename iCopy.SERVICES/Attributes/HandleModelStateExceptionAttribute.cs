using System.Net;
using System.Threading.Tasks;
using iCopy.SERVICES.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace iCopy.SERVICES.Attributes
{
    public class HandleModelStateExceptionAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ModelStateException)
            {
                ModelStateException exception = (ModelStateException)context.Exception;
                context.ModelState.AddModelError(exception.Key, exception.Message);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
    }
}
