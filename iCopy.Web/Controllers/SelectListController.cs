using System.Collections.Generic;
using System.Threading.Tasks;
using iCopy.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace iCopy.Web.Controllers
{
    public class SelectListController : Controller
    {
        public readonly ISelectList SelectList;

        public SelectListController(ISelectList SelectList)
        {
            this.SelectList = SelectList;
        }

        public async Task<IEnumerable<SelectListItem>> Cities()
        {
            return await SelectList.Cities();
        }

        public async Task<IEnumerable<SelectListItem>> CitiesByCountry(int id)
        {
            return await SelectList.Cities(id);
        }

        public async Task<IEnumerable<SelectListItem>> Companies()
        {
            return await SelectList.Companies();
        }
        public async Task<IEnumerable<SelectListItem>> CopiersByCompanyId(int id)
        {
            return await SelectList.Copiers(id);
        }
    }
}
