using iCopy.Database.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace iCopy.SERVICES.Attributes
{
    public class TransactionAttribute : ActionFilterAttribute
    {
        private DBContext dbcontext;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            dbcontext = context.HttpContext.RequestServices.GetService<DBContext>();
            IDbContextTransaction transaction = dbcontext.Database.CurrentTransaction ?? await dbcontext.Database.BeginTransactionAsync();
            var result = await next();
            if (result.Exception == null)
            {
                transaction.Commit();
                // TODO: Dodati log operaciju
            }
            else
            {
                transaction.Rollback();
                // TODO: Dodati log operaciju
            }
            transaction.Dispose();
        }
    }
}
